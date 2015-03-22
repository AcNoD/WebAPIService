using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;

namespace WebAPIService
{
    /// <summary>
    /// Service host with config loading
    /// </summary>
    class ConfigurableServiceHost : ServiceHost
    {
        public ConfigurableServiceHost(Type serviceType, params Uri[] baseAddresses)
        : base(serviceType, baseAddresses) { }

        /// <summary>
        /// Applies configurations from config to service host
        /// </summary>
        protected override void ApplyConfiguration()
        {
            var filemap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
                };

            var config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            var serviceModel = ServiceModelSectionGroup.GetSectionGroup(config);

            if (serviceModel != null)
                foreach (ServiceElement se in serviceModel.Services.Services)
                {
                    if (se.Name != Description.ConfigurationName) continue;
                    LoadConfigurationSection(se);
                    return;
                }

            throw new ArgumentException("ServiceElement doesn't exist");    
        }
    }
}
