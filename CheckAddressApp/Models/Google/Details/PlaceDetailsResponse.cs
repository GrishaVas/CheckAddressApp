﻿namespace CheckAddressApp.Models.Google.Details
{
    public class PlaceDetailsResponse
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public PlaceDetailsResponseDisplayName DisplayName { get; set; }
        public string[] Types { get; set; }
        public string PrimaryType { get; set; }
        public PlaceDetailsResponseDisplayName PrimaryTypeDisplayName { get; set; }
        public string NationalPhoneNumber { get; set; }
        public string InternationalPhoneNumber { get; set; }
        public string FormattedAddress { get; set; }
        public string ShortFormattedAddress { get; set; }
        public PlaceDetailsResponseAddressComponent[] AddressComponents { get; set; }
        public PlaceDetailsResponsePlusCode PlusCode { get; set; }
        public PlaceDetailsResponseLatLng Location { get; set; }
        public PlaceDetailsResponseViewport Viewport { get; set; }
        public double Rating { get; set; }
        public string GoogleMapsUri { get; set; }
        public string WebsiteUri { get; set; }
        public PlaceDetailsResponseReview[] Reviews { get; set; }
        public PlaceDetailsResponseOpeningHours RegularOpeningHours { get; set; }
        public PlaceDetailsResponsePhoto[] Photos { get; set; }
        public string AdrFormatAddress { get; set; }
        public string BusinessStatus { get; set; }
        public string PriceLevel { get; set; }
        public PlaceDetailsResponseAttribution[] Attributions { get; set; }
        public string IconMaskBaseUri { get; set; }
        public string IconBackgroundColor { get; set; }
        public PlaceDetailsResponseOpeningHours CurrentOpeningHours { get; set; }
        public PlaceDetailsResponseOpeningHours[] CurrentSecondaryOpeningHours { get; set; }
        public PlaceDetailsResponseOpeningHours[] RegularSecondaryOpeningHours { get; set; }
        public PlaceDetailsResponseLocalizedText EditorialSummary { get; set; }
        public PlaceDetailsResponseParkingOptions ParkingOptions { get; set; }
        public PlaceDetailsResponseSubDestination[] SubDestinations { get; set; }
        public PlaceDetailsResponseFuelOptions FuelOptions { get; set; }
        public PlaceDetailsResponseEvCharegeOptions EvChargeOptions { get; set; }
        public PlaceDetailsResponseGenerativeSummary GenerativeSummary { get; set; }
        public PlaceDetailsResponseAreaSummary AreaSummary { get; set; }
        public PlaceDetailsResponseContainingPlaces[] ContainingPlaces { get; set; }
        public PlaceDetailsResponseAddressDescriptor AddressDescriptor { get; set; }
        public PlaceDetailsResponseGoogleMapsLinks GoogleMapsLinks { get; set; }
        public PlaceDetailsResponsePriceRange PriceRange { get; set; }
        public int UtcOffsetMinutes { get; set; }
        public int UserRatingCount { get; set; }
        public bool Takeout { get; set; }
        public bool Delivery { get; set; }
        public bool DineIn { get; set; }
        public bool CurbsidePickup { get; set; }
        public bool Reservable { get; set; }
        public bool ServesLunch { get; set; }
        public bool ServesDinner { get; set; }
        public bool ServesBeer { get; set; }
        public bool ServesWine { get; set; }
        public bool ServesBrunch { get; set; }
        public bool ServesVegetarianFood { get; set; }
        public bool OutdoorSeating { get; set; }
        public bool LiveMusic { get; set; }
        public bool MenuForChildren { get; set; }
        public bool ServesCocktails { get; set; }
        public bool ServesDessert { get; set; }
        public bool ServesCoffee { get; set; }
        public bool GoodForChildren { get; set; }
        public bool AllowsDogs { get; set; }
        public bool Restroom { get; set; }
        public bool GoodForGroups { get; set; }
        public bool GoodForWatchingSports { get; set; }
        public PlaceDetailsResponseAccessibilityOptions AccessibilityOptions { get; set; }
        public bool PureServiceAreaBusiness { get; set; }
    }
}