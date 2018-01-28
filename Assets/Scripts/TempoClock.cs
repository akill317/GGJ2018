using UnityEngine;
using System.Collections;

public class TempoClock : MonoBehaviour {

    public static TempoClock Instance;
    void Awake()
    {
        Instance = this;
        secondsPerMeasure = (60 / BPM * 4);
        samplesPerMeasure = secondsPerMeasure * samplerate;
    }
        
    public double BPM;
   public float waitBeforeStart = 1f;
   public double samplerate;
    public double secondsPerMeasure;
   public double samplesPerMeasure;
    public double starttime;
    public int sixteenthcount=0;
    public int eighthcount = 0;
    public int quartercount = 0;
    public int halfcount = 0;
    public int measurecount = 0;
    public double nextMeaure;
    public double nextSixteenth;
    public double nextEighth;
    public double nextQuarter;
    public double nextHalf;
    public double TimeOfLastBeat;

    public class BeatEventArgs
    {
        public enum EBeatType{Sixteenth, Eighth, Quarter, Half, Full, Measure };
        public EBeatType BeatType;
        public int BeatID;
        public double BeatTime;
        public double NextBeatTime;
        
        public BeatEventArgs() { }
        public BeatEventArgs(EBeatType beatType, int beatID, double beatTime, double nextBeatTime)
        {
            BeatType = beatType;
            BeatID = beatID;
            BeatTime = beatTime;
            NextBeatTime = nextBeatTime;
        }

    }
    public delegate void BeatEvent(object sender, BeatEventArgs args);
    public event BeatEvent Beat;
    public event BeatEvent Sixteenth;
    public event BeatEvent Eighth;
    public event BeatEvent Quarter;
    public event BeatEvent Half;
    public event BeatEvent Measure;

	void Start () {
        starttime = AudioSettings.dspTime;
        nextMeaure = starttime + secondsPerMeasure;
        nextSixteenth = starttime + secondsPerMeasure / 16;
        nextEighth = starttime + secondsPerMeasure / 8;
        nextQuarter = starttime + secondsPerMeasure / 4;
        nextHalf = starttime + secondsPerMeasure / 2;
	}
	
	void Update () {
        secondsPerMeasure = (60 / BPM * 4);

		if (AudioSettings.dspTime > nextMeaure)
		{
			measurecount++;
			if (Measure != null)
				Measure(this, new BeatEventArgs(BeatEventArgs.EBeatType.Measure, 0, nextMeaure, nextMeaure + secondsPerMeasure));
			if (Beat != null)
				Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Measure, 0, nextMeaure, nextMeaure + secondsPerMeasure));
			nextMeaure += secondsPerMeasure;
		}

		if (AudioSettings.dspTime > nextHalf)
		{
			halfcount++;
			if (Half != null)
				Half(this, new BeatEventArgs(BeatEventArgs.EBeatType.Half, 0, nextHalf, nextHalf + secondsPerMeasure / 2));
			if (Beat != null)
				Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Half, 0, nextHalf, nextHalf + secondsPerMeasure / 2));
			nextHalf += secondsPerMeasure / 2;
		}

		if (AudioSettings.dspTime > nextQuarter)
		{
			TimeOfLastBeat = Time.time;
			quartercount++;
			if (Quarter != null)
				Quarter(this, new BeatEventArgs(BeatEventArgs.EBeatType.Quarter, 0, nextQuarter, nextQuarter + secondsPerMeasure / 4));
			if (Beat != null)
				Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Quarter, 0, nextQuarter, nextQuarter + secondsPerMeasure / 4));
			nextQuarter += secondsPerMeasure / 4;
		}

		if (AudioSettings.dspTime > nextEighth)
		{
			eighthcount++;
			if (Eighth != null)
				Eighth(this, new BeatEventArgs(BeatEventArgs.EBeatType.Eighth, 0, nextEighth, nextEighth + secondsPerMeasure / 8));
			if (Beat != null)
				Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Eighth, 0, nextEighth, nextEighth + secondsPerMeasure / 8));
			nextEighth += secondsPerMeasure / 8;
		}

        if (AudioSettings.dspTime > nextSixteenth)
        {
            sixteenthcount++;
            if (Sixteenth != null)
                Sixteenth(this, new BeatEventArgs(BeatEventArgs.EBeatType.Sixteenth, 0, nextSixteenth, nextSixteenth+secondsPerMeasure/16));
            if (Beat != null)
                Beat(this, new BeatEventArgs(BeatEventArgs.EBeatType.Sixteenth, 0, nextSixteenth, nextSixteenth + secondsPerMeasure / 16));
            nextSixteenth += secondsPerMeasure / 16;
        }
	}
}
