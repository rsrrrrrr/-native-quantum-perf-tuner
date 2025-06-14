
using LibreHardwareMonitor.Hardware;
using System.Runtime.InteropServices;
using System.Text.Json;
class NativePerfService
{
    const string NvmlDll = "nvml.dll";
    [DllImport(NvmlDll, CallingConvention = CallingConvention.Cdecl)]
    static extern int nvmlInit_v2();
    [DllImport(NvmlDll, CallingConvention = CallingConvention.Cdecl)]
    static extern int nvmlDeviceGetHandleByIndex(int index, ref IntPtr device);
    [DllImport(NvmlDll, CallingConvention = CallingConvention.Cdecl)]
    static extern int nvmlDeviceGetUtilizationRates(IntPtr device, ref NvmlUtilization util);

    struct NvmlUtilization { public uint gpu; public uint memory; }

    static Computer pc = new()
    {
        IsCpuEnabled = true,
        IsMotherboardEnabled = true,
        IsGpuEnabled = true,
        IsMemoryEnabled = true
    };

    static void Main()
    {
        pc.Open();
        nvmlInit_v2();

        while (true)
        {
            var payload = new
            {
                time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                cpu = ReadCpu(),
                gpu = ReadGpu()
            };
            Console.WriteLine(JsonSerializer.Serialize(payload));
            Thread.Sleep(1000);
        }
    }

    static object ReadCpu()
    {
        float temp = 0, load = 0;
        foreach (var hw in pc.Hardware.Where(h => h.HardwareType == HardwareType.Cpu))
        {
            hw.Update();
            temp = hw.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Temperature)?.Value ?? 0;
            load = hw.Sensors.FirstOrDefault(s => s.SensorType == SensorType.Load && s.Name == "CPU Total")?.Value ?? 0;
        }
        return new { temp, load };
    }

    static object ReadGpu()
    {
        IntPtr dev = IntPtr.Zero;
        nvmlDeviceGetHandleByIndex(0, ref dev);
        NvmlUtilization util = new();
        nvmlDeviceGetUtilizationRates(dev, ref util);
        return new { gpuLoad = util.gpu, memLoad = util.memory };
    }
}
