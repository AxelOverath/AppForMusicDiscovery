using System;
using System.Collections.Generic;
using System.Globalization;

namespace MusicDiscoveryApp
{
    public partial class RegisterInfo : ContentPage
    {
        public RegisterInfo()
        {
            InitializeComponent();

            // Populate the Picker with country names
            PopulateCountryPicker();
        }

        private void PopulateCountryPicker()
        {
            // Get all countries using CultureInfo
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            List<string> countryNames = new List<string>();

            foreach (CultureInfo culture in cultures)
            {
                RegionInfo region = new RegionInfo(culture.Name);
                if (!countryNames.Contains(region.EnglishName))
                {
                    countryNames.Add(region.EnglishName);
                }
            }

            // Sort the country names
            countryNames.Sort();

            // Add country names to the Picker
            foreach (string countryName in countryNames)
            {
                CountryPicker.Items.Add(countryName);
            }
        }

        // Other methods and event handlers...
    }
}
