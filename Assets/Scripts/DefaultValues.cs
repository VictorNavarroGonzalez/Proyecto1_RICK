using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultValues {

    public class Circle {
        public const float JumpForce = 500f;
        public const float SideForce = 100f;
        public const float DashForce = 100f;
        public const float elasticConstant = 1f;
    }


    public class Square {
        public const float JumpForce = 450f;
        public const float SideForce = 50f;
        public const float FallForce = 2000f;
        public const float DashForce = 100f;
    }

    public class Volume {
        public const float Jump = 0.2f;
        public const float Dash = 0.35f;
        public const float Smack = 1f;
        public const float Bounce = 0.2f;
    }

    public class Levels {
        private static Vector3 _current;
        public static Vector3 Current {
            get { return _current; }
            set { _current = value; }
        }


        public static Vector3 C01 { get { return new Vector3(-84, -2.75f); } }
        public static Vector3 C02 { get { return new Vector3(782, 204); } }
        public static Vector3 C03 { get { return new Vector3(1030, 324); } }

        public static Vector3 S01 { get { return new Vector3(-357, -27); } }
        public static Vector3 S02 { get { return new Vector3(187, 48); } }
        public static Vector3 S03 { get { return new Vector3(450, 70); } }

        public static Vector3 B01 { get { return new Vector3(-295, -15); } }
        public static Vector3 B02 { get { return new Vector3(47, 20); } }
        public static Vector3 B03 { get { return new Vector3(171, 107); } }
    }

}



