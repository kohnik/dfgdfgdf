namespace task
{
    public partial class Form1 : Form
    {
        Peoples peoples;
        public Form1()
        {
            InitializeComponent();
            this.peoples = new Peoples();
            Sex.Index = 0;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if ((((Names.Text != "") && (Surname.Text != "")) && (BirthDate.Text != "")) && (Group.Text != ""))
            {
             
                this.peoples.Names = this.Names.Text;
                this.peoples.Surname = this.Surname.Text;
                this.peoples.Group = this.Group.Text;
                try
                {
                    this.peoples.BirthDate = DateTime.Parse(this.BirthDate.Text);
                    int num = dataGridView1.Rows.Add();

                    dataGridView1.Rows[num].Cells[0].Value = Names.Text;
                    dataGridView1.Rows[num].Cells[1].Value = Surname.Text;
                    dataGridView1.Rows[num].Cells[2].Value = BirthDate.Text;
                    dataGridView1.Rows[num].Cells[3].Value = Sex.Text;
                    dataGridView1.Rows[num].Cells[4].Value = Group.Text;


                    this.Names.Clear();
                    this.Surname.Clear();
                    this.Group.Clear();
                    this.BirthDate.Clear();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Cледуйте формату дд.мм.гггг", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (BdException)
                {
                    MessageBox.Show("Дата рождения превышает текущую", "Ошибка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Upload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog loadFile = new OpenFileDialog
                {
                    Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*",
                    RestoreDirectory = true
                };

                if (loadFile.ShowDialog() != DialogResult.OK) { return; }
                StreamReader sr = new StreamReader(loadFilead.FileName, Encoding.UTF8, true);
                DataSet ds = new DataSet();
                ds.ReadXml(sr);

                foreach (DataRow item in ds.Tables["Peoples"].Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["Имя"];
                    dataGridView1.Rows[n].Cells[1].Value = item["Фамилия"];
                    dataGridView1.Rows[n].Cells[2].Value = item["Дата рождения"];
                    dataGridView1.Rows[n].Cells[3].Value = item["Пол"];
                    dataGridView1.Rows[n].Cells[4].Value = item["Группа"];
                }
            }
            catch (Exception)
            { MessageBox.Show("XML файл не найден.", "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        private void Exit_Click(object sender, EventArgs e)
        {
            {

                Form form1 = new Form();
                form1.Size = new System.Drawing.Size(230, 90);
                form1.HelpButton = true;
                form1.MaximizeBox = false;
                form1.MinimizeBox = false;
                form1.FormBorderStyle = FormBorderStyle.FixedDialog;
                form1.Text = "Выход";
                form1.AcceptButton = button1;
                form1.CancelButton = button2;
                form1.StartPosition = FormStartPosition.CenterScreen;
                form1.Controls.Add(button1);
                form1.Controls.Add(button2);
                form1.ShowDialog();
                Button button1 = new Button();
                Button button2 = new Button();
                button1.Text = "Выход";
                button1.Location = new Point(28, 15);
                button1.Click += new System.EventHandler(this.button5_TestClick);
                button2.Text = "Вернуться";
                button2.Location = new Point(button1.Right + 10, button1.Top);
                

            }

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Введите данные", "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataSet dataSet = new DataSet(); 
                DataTable dataTable = new DataTable();
                dataTable.TableName = "Peoples";
                dataTable.Columns.Add("Имя");
                dataTable.Columns.Add("Фамилия");
                dataTable.Columns.Add("Дата рождения");
                dataTable.Columns.Add("Пол");
                dataTable.Columns.Add("Группа");
                dataSet.Tables.Add(dataTable);

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    DataRow dataRow = dataSet.Tables["Peoples"].NewRow();
                    dataRow["Имя"] = item.Cells[0].Value;
                    dataRow["Фамилия"] = item.Cells[1].Value;
                    dataRow["Дата рождения"] = item.Cells[2].Value;
                    dataRow["Пол"] = item.Cells[3].Value;
                    dataRow["Группа"] = item.Cells[4].Value;
                    dataSet.Tables["Peoples"].Rows.Add(row);


                    SaveFileDialog save = new SaveFileDialog
                    {
                        Filter = @"XML (*.xml) | *.xml | All files(*.*) | *.*",
                        RestoreDirectory = true
                    };

                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter sw = new StreamWriter(save.FileName, true, Encoding.UTF8);
                        dataSet.WriteXml(sw);
                        MessageBox.Show("УСПЕХ");
                    }
                        return;
                }
            }

        }
      
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            Names.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Surname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            BirthDate.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Sex.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            Group.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

    }
}


Peoples:

using System;
using System.Collections.Generic;
using System.Linq;


namespace task
{
    public class BdException : System.Exception
    {
        public Int16 code;
        public BdException(Int16 code)
        {
            this.code = code;
        }
    }
    public class Peoples
    {
        public String Surname;
        public String Group;
        public String Sex;
        public String Names;
        private DateTime bd;
        public DateTime BirthDate
        {
            set
            {
                if (value < DateTime.Now) this.bd = value;
                else throw new BdException(1);
            }
            get { return this.bd; }
        }

        public Peoples() //конструктор
        {
            this.Names = null;
            this.Surname = null;
            this.Group = null;
            this.Sex = null;
        }
        public Peoples(String name, String surName, DateTime date, String group, String sex)
        {
            this.Sex = sex;
            this.Surname = surName;
            this.Names = name;
            this.BirthDate = date;
            this.Group = group;
          
        }
    }

}

