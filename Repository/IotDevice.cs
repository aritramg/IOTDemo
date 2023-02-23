using System;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;


namespace IotDevice.Repository
{
    public class IotDevice
    {
        public static RegistryManager registryManager;
        private static string connectionString="HostName=TestIOTHubAritram.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=0h7N5pLJoQY8Cv8MeZh2GUL802SJToOkATes1MyNXR0=";
        //static Device device;
        public static async Task AddDeviceAsync(string deviceName)
        {
            Device device;
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Device name please");
            }
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device=await registryManager.AddDeviceAsync(new Device(deviceName));
            return ;
        }

        public static async Task<Device> GetDeviceAsync(string deviceId)
        {
            Device device;
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device=await registryManager.GetDeviceAsync(deviceId);
            return device;
        }
         public static async Task DeleteDeviceAsync(string deviceId)
        {
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync(deviceId);
        }
        public static async Task<Device> UpdateDeviceAsync(string deviceId)
        {
            if(string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("device id please");
            }
            Device device=new Device(deviceId);
            registryManager=RegistryManager.CreateFromConnectionString(connectionString);
            device=await registryManager.GetDeviceAsync(deviceId);
            device.StatusReason="Updated";
            device=await registryManager.UpdateDeviceAsync(device);
            return device;
        }
    }
}