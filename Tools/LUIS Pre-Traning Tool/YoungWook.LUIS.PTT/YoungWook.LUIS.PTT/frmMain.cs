using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using Newtonsoft.Json;

namespace YoungWook.LUIS.PTT
{
    public partial class frmMain : Form
    {
        //JSON entity
        private Entities.LuisEntity Luis;
        private DataSet dsResult = new DataSet();

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            this.Luis = new Entities.LuisEntity();
            this.InitJSONEntity();

            MessageBox.Show("Project Created!", "Completed");
            tabMain.SelectedIndex = 1;
        }

        private void InitJSONEntity()
        {
            this.Luis.luis_schema_version = "2.0.0";
            this.Luis.versionId = "0.1";
            this.Luis.desc = "";
            this.Luis.name = "Test";
            this.Luis.culture = "ko-kr";

            this.Luis.intents = new List<Entities.Intent>();
            this.Luis.entities = new List<Entities.Entity>();
            this.Luis.utterances = new List<Entities.Utterance>();
            this.Luis.bing_entities = new List<object>();
            this.Luis.utterances = new List<Entities.Utterance>();
            this.Luis.model_features = new List<Entities.ModelFeature>();
            this.Luis.regex_features = new List<Entities.RegexFeature>();
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            //Project를 먼저 생성하게 하는 부분
            if (this.Luis == null)
            {
                MessageBox.Show("Project를 먼저 생성해 주십시오", "프로젝트 생성");
                tabMain.SelectedIndex = 0;

                return;
            }

            //File Open Dialog
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "엑셀파일|*.xlsx";
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    string ConnectionString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=No'", openFile.FileName);

                    OleDbConnection conn = new OleDbConnection(ConnectionString);
                    conn.Open();

                    // 엑셀로부터 데이타 읽기
                    dsResult.Clear();

                    //Read Intents
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Intents$]", conn);
                    OleDbDataAdapter adpt = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);

                    dsResult.Tables.Add(ds.Tables[0].Copy());
                    dsResult.Tables[0].TableName = "Intents";

                    //Read Entities
                    cmd.CommandText = "SELECT * FROM [Entities$]";
                    ds.Clear();
                    adpt.Fill(ds);

                    dsResult.Tables.Add(ds.Tables[0].Copy());
                    dsResult.Tables[1].TableName = "Entities";

                    //Read Entity Data
                    cmd.CommandText = "SELECT * FROM [EntityData$]";
                    ds.Clear();
                    adpt.Fill(ds);

                    dsResult.Tables.Add(ds.Tables[0].Copy());
                    dsResult.Tables[2].TableName = "EntityData";

                    //Read Sentence
                    cmd.CommandText = "SELECT * FROM [Sentence$]";
                    ds.Clear();
                    adpt.Fill(ds);

                    dsResult.Tables.Add(ds.Tables[0].Copy());
                    dsResult.Tables[3].TableName = "Sentence";

                    //Databind Dataview
                    grdIntents.DataSource = dsResult.Tables[0];
                    grdEntities.DataSource = dsResult.Tables[1];
                    grdEntityData.DataSource = dsResult.Tables[2];
                    grdSentence.DataSource = dsResult.Tables[3];

                    MessageBox.Show("Excel Data Readed!", "Completed");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSaveJSON_Click(object sender, EventArgs e)
        {
            //File Open Dialog
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "JSON |*.json";
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {


                foreach (DataRow r in dsResult.Tables[0].Rows)
                {
                    string name = r[0].ToString();

                    if (name != "null")
                    {
                        Entities.Intent intent = new Entities.Intent();
                        intent.name = name;

                        this.Luis.intents.Add(intent);
                    }
                }

                foreach (DataRow r in dsResult.Tables[1].Rows)
                {
                    string name = r[0].ToString();
                    if (name != "null")
                    {
                        Entities.Entity entity = new Entities.Entity();
                        entity.name = name;

                        this.Luis.entities.Add(entity);
                    }
                }

                //Add sentence
                foreach (DataRow r in dsResult.Tables[3].Rows)
                {
                    string intent = r[0].ToString();
                    string sentence = r[1].ToString();
                    string[] temp = r[2].ToString().Split(',');

                    //Entity가 두 개짜리 추가
                    if (temp.Length == 2)
                    {
                        DataView dv1 = new DataView(dsResult.Tables[2]);
                        DataView dv2 = new DataView(dsResult.Tables[2]);

                        dv1.RowFilter = string.Format("F1='{0}'", temp[0]);
                        dv2.RowFilter = string.Format("F1='{0}'", temp[1]);

                        for (int i = 0; i < dv1.Count; i++)
                        {
                            for (int j = 0; j < dv2.Count; j++)
                            {
                                string sentenceResult = string.Format(sentence, dv1[i].Row[1].ToString(), dv2[j].Row[1].ToString());
                                Entities.Utterance utterance = new Entities.Utterance();

                                utterance.text = sentenceResult;
                                utterance.intent = intent;
                                utterance.entities = new List<Entities.Entity2>();

                                Entities.Entity2 entity1 = new Entities.Entity2();
                                entity1.startPos = sentenceResult.IndexOf(dv1[i].Row[1].ToString());
                                entity1.endPos = entity1.startPos + dv1[i].Row[1].ToString().Length;
                                entity1.entity = dv1[i].Row[0].ToString();

                                Entities.Entity2 entity2 = new Entities.Entity2();
                                entity2.startPos = sentenceResult.IndexOf(dv2[j].Row[1].ToString());
                                entity2.endPos = entity2.startPos + dv2[j].Row[1].ToString().Length;
                                entity2.entity = dv2[0].Row[0].ToString();

                                utterance.entities.Add(entity1);
                                utterance.entities.Add(entity2);

                                this.Luis.utterances.Add(utterance);
                            }
                        }
                    }

                    //Entity가 한 개짜리 추가
                    else if (temp.Length == 1)
                    {
                        DataView dv = new DataView(dsResult.Tables[2]);

                        dv.RowFilter = string.Format("F1='{0}'", temp[0]);

                        //Entity가 한 개짜리 처리
                        if (temp[0] != "")
                        {
                            for (int i = 0; i < dv.Count; i++)
                            {
                                string sentenceResult = string.Format(sentence, dv[i].Row[1].ToString());
                                Entities.Utterance utterance = new Entities.Utterance();

                                utterance.text = sentenceResult;
                                utterance.intent = intent;
                                utterance.entities = new List<Entities.Entity2>();

                                Entities.Entity2 entity = new Entities.Entity2();
                                entity.startPos = sentenceResult.IndexOf(dv[i].Row[1].ToString());
                                entity.endPos = entity.startPos + dv[i].Row[1].ToString().Length;
                                entity.entity = dv[i].Row[0].ToString();

                                utterance.entities.Add(entity);

                                this.Luis.utterances.Add(utterance);
                            }
                        }
                        else  //Entity가 없는 경우
                        {
                            Entities.Utterance utterance = new Entities.Utterance();

                            utterance.text = sentence;
                            utterance.intent = intent;
                            utterance.entities = new List<Entities.Entity2>();

                            this.Luis.utterances.Add(utterance);
                        }
                    }
                }

                string resultJson = JsonConvert.SerializeObject(this.Luis);
                txtJson.Text = resultJson;


                System.IO.File.WriteAllText(saveFile.FileName, resultJson, Encoding.UTF8);
            }
        }
    }
}
