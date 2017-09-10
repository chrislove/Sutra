namespace SharpPipe {
    public static partial class func {
        public abstract class strfunc     : takes<string> { }
        public abstract class fltfunc     : takes<float> { }
        public abstract class integerfunc : takes<int> { }
        public abstract class dblfunc     : takes<double> { }
    }
}