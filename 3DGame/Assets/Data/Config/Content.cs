using System.Collections.Generic;

namespace Content
{
    [System.Serializable]
    public class BeatData 
    {
        public List<NoteData>  noteDatas = new List<NoteData>();
        public List<NoteModifier> modifiers = new List<NoteModifier>();
        public float tempo;
    }

    [System.Serializable]
    public class NoteData
    {
        public float timestamp;
        public int type;
    }

    [System.Serializable]
    public class NoteModifier
    {
        public int type;
        public bool hitBlock;
        public int from, to;
        public int amplitude;
        public bool movable;
        public int step = 1;
        public float modificationStep;
        public float min = 4;
        public float max = 4;
        public int shockLevel;
    }
}