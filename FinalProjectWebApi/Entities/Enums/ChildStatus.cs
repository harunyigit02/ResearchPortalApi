namespace FinalProjectWebApi.Entities.Enums
{
    public enum ChildStatus
    {
        NoChildren=1,            // Çocuk yok
        OneChild=2,              // Bir çocuğa sahip
        TwoChildren=3,           // İki çocuğa sahip
        MultipleChildren=4,      // Birden fazla çocuğa sahip
        Twins=5,                 // İkiz çocuklara sahip
        SpecialNeedsChild=6,     // Özel gereksinimli çocuğa sahip
        TeenagerChild=7,         // Ergen çocuğa sahip
        InfantChild=8,           // Bebek çocuğa sahip
        AdultChildren=9,         // Yetişkin çocuğa sahip
        AdultWithOwnFamily=10,    // Çocuğu evli ve kendi ailesi olan
        Other=11
    }
}
