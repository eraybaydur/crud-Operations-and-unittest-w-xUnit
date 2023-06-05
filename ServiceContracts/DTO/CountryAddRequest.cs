﻿using Entities;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class for adding a new Country
    /// </summary>
    public class CountryAddRequest
    {
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country()
            {
                CountryName = CountryName
            };
        }
    }
}
