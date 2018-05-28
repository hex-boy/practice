namespace AvalonDockMvvm.Usage.Converter
{
    internal class BoolToNotConverter : AbstractBoolConverter<bool>
    {
        public BoolToNotConverter() :
            base(false, true) {
        }
    }
}