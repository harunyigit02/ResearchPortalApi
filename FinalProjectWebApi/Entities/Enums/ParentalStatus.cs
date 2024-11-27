namespace FinalProjectWebApi.Entities.Enums
{
    public enum ParentalStatus
    {
        NotParent,             // Ebeveyn değil               // Ebeveyn
        SingleParent,          // Tek ebeveyn
        TwoParentHousehold,    // İki ebeveynli aile
        DivorcedParent,        // Boşanmış ebeveyn
        SeparatedParent,       // Ayrı yaşayan ebeveyn
        CoParenting,           // Ortak ebeveynlik
        StepParent,            // Üvey ebeveyn
        AdoptiveParent,        // Evlat edinmiş ebeveyn
        FosterParent,          // Geçici bakıcı ebeveyn
        GrandparentAsParent,   // Büyükanne/büyükbaba ebeveyni
        Other
    }
}
