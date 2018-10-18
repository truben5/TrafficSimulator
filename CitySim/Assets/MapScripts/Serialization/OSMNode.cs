﻿using System.Xml;
using UnityEngine;

class OSMNode : BaseOSM
{
    public ulong ID { get; private set; }

    public float Latitude { get; private set; }

    public float Longitude { get; private set; }

    public bool IsStreetLight { get; private set; }

    public float X { get; private set; }

    public float Y { get; private set; }

    public static implicit operator Vector3 (OSMNode node)
    {
        return new Vector3(node.X, 0, node.Y);
    }


    public OSMNode(XmlNode node)
	{
        ID = GetAttribute<ulong>("id", node.Attributes);
        Latitude = GetAttribute<float>("lat", node.Attributes);
        Longitude = GetAttribute<float>("lon", node.Attributes);
        //string Val = GetAttribute<string>("v", node.Attributes);

        // Transfer this for waypoints?
        X = (float)MercatorProjection.lonToX(Longitude);
        Y = (float)MercatorProjection.latToY(Latitude);

        XmlNodeList tags = node.SelectNodes("tag");
        foreach (XmlNode t in tags)
        {
            string val = GetAttribute<string>("v", t.Attributes);
            //Debug.Log(val);
            if (val == "traffic_signals")
            {
                IsStreetLight = true;
            }
        }


    }

}
