using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Taxa_de_Mediu
{
    public partial class Form1 : Form
    {
        double A = 0, B = 0, C = 0, D = 0, E = 0, plata = -2;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    button1.Visible = true;
                    B = 0;
                    A = -2;
                }
            }
            catch
            {
            }
            finally
            {
                System.Windows.Forms.MessageBox.Show("Emisia de dioxid de carbon (grame CO2 / km) : Nestiut" + Environment.NewLine + " Nivelul taxei specifice (euro / 1 gram CO2) :" + Convert.ToString(B));

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton3.Checked == true)
                {
                    button3.Visible = true;
                    B = 16;
                    A = -1;
                }

            }
            catch
            {
            }
            finally
            {
                System.Windows.Forms.MessageBox.Show("Emisia de dioxid de carbon (grame CO2 / km) : Nestiut" + Environment.NewLine + " Nivelul taxei specifice (euro / 1 gram CO2) :" + Convert.ToString(B));
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
                groupBox4.Visible = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                groupBox4.Visible = false;
                System.Windows.Forms.MessageBox.Show("Capacitate cilindrica nu influenteaza suma taxei , taxa 0 ");
                D = 0; C = -1;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool ok1 = false;
            try
            {
                ok1 = double.TryParse(textBox2.Text, out C);
                C = Convert.ToDouble(textBox2.Text);
                if (C < 100 || C > 20000)
                {
                    textBox2.Clear();
                    System.Windows.Forms.MessageBox.Show("Doar valori pozitive ! ");
                    throw new Exception("Nu poate lua valori negative");

                }
                if (radioButton5.Checked == true && C <= 2000)
                {
                    D = 1.3;
                }
                else if (radioButton6.Checked == false && radioButton7.Checked == false) D = 0.39;
                if (radioButton6.Checked == true && C <= 2000)
                {
                    D = 0.13;
                }
                else if (radioButton5.Checked == false && radioButton7.Checked == false) D = 3;
                if (radioButton7.Checked == true && C <= 2000)
                {
                    D = 9;
                }
                else if (radioButton1.Checked == false && radioButton5.Checked == false) D = 16;
                System.Windows.Forms.MessageBox.Show("Capacitatea cilindrică(cmc) :" + Convert.ToString(C) + Environment.NewLine + "Nivelul taxei specifice(euro / 1 cmc) :" + Convert.ToString(D));

            }
            catch (Exception ex)
            {
                if (ok1 != true)
                {
                    C = -1;
                    System.Windows.Forms.MessageBox.Show("Valoare introdusa gresit !  " + ex.Message);
                    textBox2.Clear();
                }

            }

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                groupBox4.Visible = true;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                groupBox4.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (A == -1 || B == -1 || C == -1 || D == -1)

                    throw new Exception("Nu toare campurile au fost completate ! ");
                else
                {
                    if (radioButton8.Checked == true)
                        E = 0;
                    else if (radioButton9.Checked == true)
                        E = 10;
                    else if (radioButton10.Checked == true)
                        E = 30;
                    else if (radioButton11.Checked == true)
                        E = 40;
                    else if (radioButton12.Checked == true)
                        E = 60;
                    else if (radioButton13.Checked == true)
                        E = 80;
                }
                if (A == -1)
                    plata = (((B * 0.3) + (C * D * 0.7)) * ((100 - E) / 100)) / 100;
                if (C == -1 && A == -1)
                    plata = (((B * 0.3) + (D * 0.7)) * ((100 - E) / 100)) / 100;
                if (C != -1 && A != -1 && B != -1 && D != -1)
                    plata = (((A * B * 0.3) + (C * D * 0.7)) * ((100 - E) / 100)) / 100;

                textBox7.Visible = true;
                label9.Visible = true;
                afisare();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        void afisare()
        {
            textBox7.Text = "~Taxa echivalenta :" + Convert.ToString(plata) + "  lei" + Environment.NewLine + "~Pentru autoturismul : " + textBox4.Text + Environment.NewLine + "~Detinut de catre persoana fizica :" + textBox3.Text + Environment.NewLine + "~Model :" + textBox5.Text + Environment.NewLine + "~Din anul : " + textBox6.Text + Environment.NewLine + "~Calculata la data de :" + DateTime.Now.ToString();

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    filePath = openFileDialog.FileName;


                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {


                        try
                        {

                            fileContent = reader.ReadToEnd();

                            fileContent = fileContent.Substring(fileContent.IndexOf("=") + 1);
                            A = Convert.ToDouble(fileContent.Substring(0, fileContent.IndexOf(" ")));
                            fileContent = fileContent.Substring(fileContent.IndexOf("=") + 1);
                            B = Convert.ToDouble(fileContent.Substring(0, fileContent.IndexOf(" ")));
                            fileContent = fileContent.Substring(fileContent.IndexOf("=") + 1);
                            C = Convert.ToDouble(fileContent.Substring(0, fileContent.IndexOf(" ")));
                            fileContent = fileContent.Substring(fileContent.IndexOf("=") + 1);
                            D = Convert.ToDouble(fileContent.Substring(0, fileContent.IndexOf(" ")));
                            fileContent = fileContent.Substring(fileContent.IndexOf("=") + 1);
                            E = Convert.ToDouble(fileContent.Substring(0, fileContent.IndexOf(" ")));

                            if (E == 2 || A == 2 || B == 2 || C == 2 || D == 2)
                                throw new Exception("Eroare la citirea datelor din fisier!  ");
                            MessageBox.Show("Locatia Fisierului: " + filePath + Environment.NewLine + "~Emisia de dioxid de carbon (grame CO2 / km) : " + Convert.ToString(A) + Environment.NewLine + "~Nivelul taxei specifice (euro / 1 gram CO2) :" + Convert.ToString(B) + Environment.NewLine + "~Capacitatea cilindrică(cmc) :" + Convert.ToString(C) + Environment.NewLine + "~Nivelul taxei specifice(euro / 1 cmc) :" + Convert.ToString(D) + Environment.NewLine + "~Reducerea aferenta :" + Convert.ToString(E), "~Valorile obtinute din fisier : ", MessageBoxButtons.OK);
                            button5.PerformClick();

                        }


                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("Eroare: " + ex.Message);
                        }


                    }
                }
            }


        }

        private void DinFisierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var var1 = string.Empty;
            var var2 = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    var2 = openFileDialog.FileName;


                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {


                        try
                        {

                            var1 = reader.ReadToEnd();

                            var1 = var1.Substring(var1.IndexOf("=") + 1);
                            A = Convert.ToDouble(var1.Substring(0, var1.IndexOf(" ")));
                            var1 = var1.Substring(var1.IndexOf("=") + 1);
                            B = Convert.ToDouble(var1.Substring(0, var1.IndexOf(" ")));
                            var1 = var1.Substring(var1.IndexOf("=") + 1);
                            C = Convert.ToDouble(var1.Substring(0, var1.IndexOf(" ")));
                            var1 = var1.Substring(var1.IndexOf("=") + 1);
                            D = Convert.ToDouble(var1.Substring(0, var1.IndexOf(" ")));
                            var1 = var1.Substring(var1.IndexOf("=") + 1);
                            E = Convert.ToDouble(var1.Substring(0, var1.IndexOf(" ")));

                            if (E == 2 || A == 2 || B == 2 || C == 2 || D == 2)
                                throw new Exception("A aparut o eroare la citirea datelor din fisier .  ");
                            MessageBox.Show("Locatia Fisierului: " + var2 + Environment.NewLine + "~Emisia de dioxid de carbon (grame CO2 / km) : " + Convert.ToString(A) + Environment.NewLine + "~Nivelul taxei specifice (euro / 1 gram CO2) :" + Convert.ToString(B) + Environment.NewLine + "~Capacitatea cilindrică(cmc) :" + Convert.ToString(C) + Environment.NewLine + "~Nivelul taxei specifice(euro / 1 cmc) :" + Convert.ToString(D) + Environment.NewLine + "~Reducerea aferenta :" + Convert.ToString(E), "~Valorile obtinute din fisier : ", MessageBoxButtons.OK);
                            button5.PerformClick();

                        }


                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show("Ceva nu a decurs bine . Error : " + ex.Message);
                        }


                    }
                }
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            afisare();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            afisare();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            afisare();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Doriti sa inchideti aplicatia ?  ", "Inchide ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)

            {
                Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            afisare();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            afisare();
        }

        private void button6_Click(object sender, EventArgs e)
        {


            string info = Convert.ToString(plata);

            var save = new SaveFileDialog();

            save.Filter = "txt files (*.txt)|*.txt";
            save.FilterIndex = 2;
            save.FileName = textBox3.Text;

            DialogResult result = MessageBox.Show("Doriti sa adaugati informatiile intr-un fisier existent (Yes) sau creati unul nou (No) ?  ", "Modalitate de scriere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {

                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    if (!string.IsNullOrWhiteSpace(textBox5.Text))
                        if (!string.IsNullOrWhiteSpace(textBox5.Text))
                        {
                            if (!string.IsNullOrWhiteSpace(textBox6.Text))
                            {
                                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                                {
                                    if (result == DialogResult.No)
                                    {
                                        {




                                            if (save.ShowDialog() == DialogResult.OK)
                                            {
                                                File.WriteAllText(save.FileName, " Taxa echivalenta " + Convert.ToString(plata) + "  pentru autoturismul: " + textBox5.Text + ", " + textBox2.Text + " din anul: " + textBox4.Text + " detinut de catre persoana fizica: " + textBox1.Text);
                                                System.Windows.Forms.MessageBox.Show("Datele au fost salvate cu succes in fisierul " + save.FileName);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        OpenFileDialog openFileDialog1 = new OpenFileDialog();
                                        if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                        {
                                            string path = openFileDialog1.FileName;
                                            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Append);
                                            StreamWriter sw = new StreamWriter(fs);
                                            sw.WriteLine(Environment.NewLine + Environment.NewLine + "~Taxa echivalenta :" + Convert.ToString(plata) + "  lei" + Environment.NewLine + "~Pentru autoturismul : " + textBox5.Text + Environment.NewLine + "~Model :" + textBox2.Text + Environment.NewLine + "~Din anul : " + textBox4.Text + Environment.NewLine + "~Detinut de catre persoana fizica :" + textBox1.Text + Environment.NewLine + "~Calculata la data de :" + DateTime.Now.ToString() + Environment.NewLine);
                                            sw.Flush();
                                            sw.Close();
                                            System.Windows.Forms.MessageBox.Show("Datele au fost salvate in fisierul " + path);
                                        }


                                    }
                                }
                            }
                        }

                }


                else { throw new Exception("Propietar/Marca/Model/An NULL"); }
            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox1.Visible = true;
                button2.Visible = true;
                button1.Visible = false;
                button3.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                textBox1.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = true;
                label3.Visible = true;
                label2.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox1.Visible = false;
                button2.Visible = false;
                label2.Visible = true;
                button1.Visible = true;
                button3.Visible = false;
                label3.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool ok = false;
            try
            {
                ok = double.TryParse(textBox1.Text, out A);
                A = Convert.ToDouble(textBox1.Text);
                if (A >= 0)
                {
                    if (A >= 0 && A <= 120)
                    {
                        B = 0;


                    }
                    else if (A <= 210 && A >= 121)
                    {
                        B = 1;

                    }
                    else if (A >= 211 && A <= 270)
                    {
                        B = 4;

                    }
                    else if (A > 270)
                    {
                        B = 8;

                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Se accepta doar valori pozitive ! ");
                    throw new Exception("Nu sunt permise valorile negative !");
                }
                System.Windows.Forms.MessageBox.Show("Emisia de dioxid de carbon (grame CO2 / km) :" + Convert.ToString(A) + Environment.NewLine + "Nivelul taxei specifice (euro / 1 gram CO2) :" + Convert.ToString(B));
            }
            catch (Exception ex)
            {

                if (ok != true)
                {
                    A = 0; B = 0;
                    System.Windows.Forms.MessageBox.Show("Valoare introdusa este gresita!" + ex.Message);
                    textBox1.Clear();
                }

            }
        }


        public Form1()
        {
            InitializeComponent();
            radioButton8.Checked = true;
        }


    }
}
