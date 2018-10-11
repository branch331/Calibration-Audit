using System;
using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.CalibrationAudit
{
    internal class HardwareViewModel 
    {
        /// <summary>
        /// Class for each device in the system that contains internal and external calibration data, and temperature.
        /// </summary>
        private string notAvailableConstant = "N/A";

        public HardwareViewModel(ProductResource resource)
        {
            UserAlias = resource.UserAlias;
            NumberOfExperts = resource.Experts.Count;
            ExpertResourceName = resource.Experts[0].ResourceName;
            ExpertProgrammaticName = resource.Experts[0].ExpertProgrammaticName;
            Model = resource.ProductName;
            SerialNumber = resource.SerialNumber;

            if (resource.SupportsInternalCalibration)
            {
                ShowInternalCalData(resource);
            }
            else
            {
                InternalLastCalDate = notAvailableConstant;
                InternalLastCalTemp = notAvailableConstant;
            }

            if (resource.SupportsExternalCalibration)
            {
                ShowExternalCalData(resource);
            }
            else
            {
                ExternalLastCalDate = notAvailableConstant;
                ExternalLastCalTemp = notAvailableConstant;
                RecommendedNextCal = notAvailableConstant;
            }

            ShowTemperatureData(resource);
        }

        // Helper functions use try catch blocks to detect System Configuration exceptions.
        // If a value is not supported by the device (returns an exception) then "N/A" is returned.

        public void ShowInternalCalData(ProductResource resource)
        {
            try
            {
                InternalLastCalDate = resource.InternalCalibrationDate.ToString("MM-yyyy");
                InternalLastCalTemp = resource.InternalCalibrationTemperature.ToString("0.00");
            }
            catch (SystemConfigurationException ex)
            {
                InternalLastCalDate = notAvailableConstant;
                InternalLastCalTemp = notAvailableConstant;
                Error = ex.ErrorCode.ToString();
            }
        }

        public void ShowExternalCalData(ProductResource resource)
        {
            CalibrationOverdue = false;

            try
            {
                ExternalLastCalDate = resource.ExternalCalibrationDate.ToString("MM-yyyy");
            }
            catch (SystemConfigurationException ex)
            {
                ExternalLastCalDate = notAvailableConstant;
                Error = ex.ErrorCode.ToString();
            }

            try
            {
                ExternalLastCalTemp = resource.ExternalCalibrationTemperature.ToString("0.00");
            }
            catch (SystemConfigurationException ex)
            {
                ExternalLastCalTemp = notAvailableConstant;
                Error = ex.ErrorCode.ToString();
            }
            try
            {
                RecommendedNextCal = resource.ExternalCalibrationDueDate.ToString("MM-yyyy");
                CalibrationOverdue = resource.ExternalCalibrationDueDate < DateTime.Now;
            }
            catch (SystemConfigurationException ex)
            {
                RecommendedNextCal = notAvailableConstant;
                Error = ex.ErrorCode.ToString();
            }
        }

        public void ShowTemperatureData(ProductResource productResource)
        {
            try
            {
                TemperatureSensor[] sensors = productResource.QueryTemperatureSensors(SensorInfo.Reading);
                Temperature = sensors[0].Reading.ToString("0.00"); // Sensor 0 is the internal temperature.
            }
            catch (SystemConfigurationException ex)
            {
                Temperature = notAvailableConstant;
                Error = ex.ErrorCode.ToString();
            }
        }

        public string UserAlias
        {
            get;
            private set;
        }

        public string ExpertResourceName
        {
            get;
            private set;
        }

        public string ExpertProgrammaticName
        {
            get;
            private set;
        }

        public string Model
        {
            get;
            private set;
        }

        public string SerialNumber
        {
            get;
            private set;
        }

        public string InternalLastCalDate
        {
            get;
            private set;
        }

        public string InternalLastCalTemp
        {
            get;
            private set;
        }

        public string ExternalLastCalDate
        {
            get;
            private set;
        }

        public string ExternalLastCalTemp
        {
            get;
            private set;
        }

        public string RecommendedNextCal
        {
            get;
            private set;
        }

        public string Temperature
        {
            get;
            private set;
        }

        public string Error
        {
            get;
            private set;
        }

        public bool CalibrationOverdue
        {
            get;
            private set;
        }

        public int NumberOfExperts
        {
            get;
            private set;
        }
    }
}
