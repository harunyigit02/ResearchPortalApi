namespace FinalProjectWebApi.Entities.Enums
{
    public enum ParentalStatus
    {
        NotParent=1,             // Ebeveyn değil
        SingleParent=2,          // Tek ebeveyn
        TwoParentHousehold=3,    // İki ebeveynli aile
        DivorcedParent=4,        // Boşanmış ebeveyn
        SeparatedParent=5,       // Ayrı yaşayan ebeveyn
        CoParenting=6,           // Ortak ebeveynlik
        StepParent=7,            // Üvey ebeveyn
        AdoptiveParent=8,        // Evlat edinmiş ebeveyn
        FosterParent=9,          // Geçici bakıcı ebeveyn
        GrandparentAsParent=10,   // Büyükanne/büyükbaba ebeveyni
        Other=11
    }
}
