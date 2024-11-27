namespace FinalProjectWebApi.Entities.Enums
{
    public enum ChildStatus
    {
        NoChildren,            // Çocuk yok
        OneChild,              // Bir çocuğa sahip
        TwoChildren,           // İki çocuğa sahip
        MultipleChildren,      // Birden fazla çocuğa sahip
        Twins,                 // İkiz çocuklara sahip
        SpecialNeedsChild,     // Özel gereksinimli çocuğa sahip
        TeenagerChild,         // Ergen çocuğa sahip
        InfantChild,           // Bebek çocuğa sahip
        AdultChildren,         // Yetişkin çocuğa sahip
        AdultWithOwnFamily,    // Çocuğu evli ve kendi ailesi olan
        Other
    }
}
