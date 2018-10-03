using NationalInstruments.SystemConfiguration;
using System;

namespace NationalInstruments.Examples.CalibrationAudit
{
    internal class HardwareViewModel 
    {
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
                InternalLastCalDate = "N/A";
                InternalLastCalTemp = "N/A";
            }

            if (resource.SupportsExternalCalibration)
            {

                ShowExternalCalData(resource);
            }
            else
            {
                ExternalLastCalDate = "N/A";
                ExternalLastCalTemp = "N/A";
                RecommendedNextCal = "N/A";
            }

            ShowTemperatureData(resource);
        }

        // Helper functions use try catch blocks to detect System Configuration exceptions.
        // If a value is not supported by the device (returns an exception) then "N/A" is returned.

        public void ShowInternalCalData(ProductResource resource)
        {
            try
            {
                InternalLastCalDate = resource.InternalCalibrationDate.ToString("MM-dd-yyyy");
                InternalLastCalTemp = resource.InternalCalibrationTemperature.ToString("0.00");
            }
            catch (SystemConfigurationException ex)
            {
                InternalLastCalDate = "N/A";
                InternalLastCalTemp = "N/A";
                Error = ex.ErrorCode.ToString();
            }
        }

        public void ShowExternalCalData(ProductResource resource)
        {
            CalibrationOverdue = false;

            try
            {
                ExternalLastCalDate = resource.ExternalCalibrationDate.ToString("MM-dd-yyyy");
            }
            catch (SystemConfigurationException ex)
            {
                ExternalLastCalDate = "N/A";
                Error = ex.ErrorCode.ToString();
            }

            try
            {
                ExternalLastCalTemp = resource.ExternalCalibrationTemperature.ToString("0.00");
            }
            catch (SystemConfigurationException ex)
            {
                ExternalLastCalTemp = "N/A";
                Error = ex.ErrorCode.ToString();
            }
            try
            {
                RecommendedNextCal = resource.ExternalCalibrationDueDate.ToString("MM-dd-yyyy");
                CalibrationOverdue = resource.ExternalCalibrationDueDate < DateTime.Now;
            }
            catch (SystemConfigurationException ex)
            {
                RecommendedNextCal = "N/A";
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
                Temperature = "N/A";
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
