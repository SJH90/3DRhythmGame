using System.Collections;
using System.Collections.Generic;

public static class MusicData
{

    public static string Name { get; set; }
    public static string FileName { get; set; }
    public static float Bpm { get; set; }
    public static float Preset { get; set; }
    public static float Speed { get; set; }

    public static List<float> laneList1 = new List<float>();
    public static List<float> laneList2 = new List<float>();
    public static List<float> laneList3 = new List<float>();
    public static List<float> laneList4 = new List<float>();

    public static void Reset()
    {
        Name = "";
        FileName = "";
        Bpm = 0;
        //Preset = 0;
        //Speed = 1;

        laneList1.Clear();
        laneList2.Clear();
        laneList3.Clear();
        laneList4.Clear();
    }

    public static void addLane1(float time)
    {
        float time2 = time * (float)60 * 4 / Bpm;
        laneList1.Add(time2 - Preset);
    }
    public static void addLane2(float time)
    {
        float time2 = time * (float)60 * 4 / Bpm;
        laneList2.Add(time2 - Preset);
    }
    public static void addLane3(float time)
    {
        float time2 = time * (float)60 * 4 / Bpm;
        laneList3.Add(time2 - Preset);
    }
    public static void addLane4(float time)
    {
        float time2 = time * (float)60 * 4 / Bpm;
        laneList4.Add(time2 - Preset);
    }

    public static void SetSpeed(float speed)
    {
        Speed = speed;
    }

}
