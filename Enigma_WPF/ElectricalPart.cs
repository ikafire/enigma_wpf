
namespace Enigma_WPF
{
    /// <summary>
    /// Enigma machine中所有電子零件的母類別
    /// </summary>
    public abstract class ElectricalPart
    {
        protected int[] wiring;

        public PartType Type { get; protected set; }

        public string Name { get; set; }
    }
}