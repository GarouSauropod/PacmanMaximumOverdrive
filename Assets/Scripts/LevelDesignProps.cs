using System.Collections;
using System.Collections.Generic;
using DataUtility;
using UnityEngine;

namespace LevelDesignProps
{

    [System.Serializable]
	public class LevelProp
    {
        public string name;
        public GameObject physicalObject;
        public Vector3 positionOffset;
        public IntVector2[] impassableSquares;

        public LevelProp(string _name, GameObject _prop, Vector3 _offset, IntVector2[] _impassableSquares)
        {
            name = _name;
            physicalObject = _prop;
            positionOffset = _offset;
            impassableSquares = _impassableSquares;
        }
    }
}
