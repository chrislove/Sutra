namespace SharpPipe {
    public static partial class Commands {
        public static partial class func {
            public abstract class str : takes<string> { }

            public abstract class flt : takes<float> { }

            public abstract class integer : takes<int> { }

            public abstract class dbl : takes<double> { }
        }
    }
}