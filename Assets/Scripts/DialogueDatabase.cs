using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class DialogueDatabase : MonoBehaviour {

        private List<Dialogue> database = new List<Dialogue>();
        private JsonData dialogueData;

        void Start()
        {
            dialogueData = JsonMapper.ToObject (File.ReadAllText(Application.streamingAssetsPath + "/Dialogue.json"));

            ConstructItemDatabase ();
        }

        public List<Dialogue> FetchDialogueListByNPCID(int npc_idToFind)
        {

            List<Dialogue> npc_dialogue = new List<Dialogue>();

            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].NPC_ID == npc_idToFind)
                {
                    npc_dialogue.Add(database[i]);
                }
            }
            
            if (npc_dialogue.Count != 0)
            {
                return npc_dialogue;
            }
            else
            {
                return null;
            }
        }

        void ConstructItemDatabase()
        {
            for (int i = 0; i < dialogueData.Count; i++) 
            {
                database.Add (new Dialogue (
                (int)dialogueData[i]["npc_id"],
                dialogueData[i]["npc_name"].ToString(),
                (int)dialogueData[i]["dialogue_id"],  
                dialogueData[i]["dialogue_text"].ToString()
                ));
            }
        }
    }

    public class Dialogue
    {
        public int NPC_ID { get; set; }
        public string NPC_Name { get; set; }
        public int Dialogue_ID { get; set; }
        public string Dialogue_Text { get; set; }

    public Dialogue(int npc_id, string npc_name, int dialogue_id, string dialogue_text)
        {
            this.NPC_ID = npc_id;
            this.NPC_Name = npc_name;
            this.Dialogue_ID = dialogue_id;
            this.Dialogue_Text = dialogue_text;
        }

        public Dialogue()
        {
            this.NPC_ID = -1;
        }
    }