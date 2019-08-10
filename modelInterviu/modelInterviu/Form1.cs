using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modelInterviu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int a, b;
        double sum1, sum2, sumtime1, sumtime2, sumYes1, sumYes2;
        double[,] masInterval, masDistribution, masTime, masTimeInteval, masYes, masItog;
        Random rnd = new Random();

        private void btnRand_Click(object sender, EventArgs e)
        {
            rand(lstR1);
            rand(lstR2);
        }

        private void rand(ListBox lst)
        {
            int a=20;
            int r;
            for (int i = 0; i < a; i++)
            {
                r = rnd.Next(1,99);

                if (lst.Items.Contains(r))
                {
                    a++;
                }
                else
                {
                    lst.Items.Add(r);
                }
                if (lst.Items.Count == 20)
                {
                    break;
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Otkaz=0;
            int Owremja=0;

            for (int i = 0; i < 20; i++)
            {
                if (masItog[7, i] == 0)
                {

                    Otkaz++;
                }
                else
                {
                    if (i == 19)
                    { break; }
                    if (masItog[8, i + 1] < masItog[9, i])
                    {
                        Owremja++;
                        masItog[9, i + 1] = 0;
                        masItog[8, i + 1] = 0;
                        for (int k = i+2; k < 20; k++)
                        {
                            if (masItog[8, k] <masItog[9, i])
                            {
                                Owremja++;
                                masItog[9, i + 1] = 0;
                                masItog[8, i + 1] = 0;
                            }
                        }
                    }
                  

                }

                


            }

            if (Otkaz < Owremja)
            {
                lbl1.Text = "GG Рабочий нужен";
            }
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    dgvItog[j, i].Value = masItog[j, i].ToString("0.##");
                }

            }

        }

        private void btnItog_Click(object sender, EventArgs e)
        {
            masItog = new double[10, 20];
           int nom=0;
            int nom2 = 0;

            for (int i = 0; i < 20; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dgvItog.Rows.Add(dr);
            }

            for (int i = 0; i < 20; i++)
            {
                masItog[0, i] = i;

               

                masItog[1, i] =Convert.ToInt32 (lstR1.Items[i]);

                masItog[2, i] = Convert.ToInt32(interval(Convert.ToInt32(masItog[1, i]), masDistribution, Convert.ToInt32(masItog[2, i])));
                if (i == 0)
                {
                    masItog[3, i] = masItog[2, i];
                }
                else
                {
                    masItog[3, i] = masItog[3, i - 1] + masItog[2, i];
                }
                masItog[4, i] = Convert.ToInt32(lstR2.Items[i]);

                /////5555
                masItog[5, i] = Convert.ToInt32(interval(Convert.ToInt32(masItog[4, i]), masYes, Convert.ToInt32(masItog[5, i])));
                if (masItog[5, i] == 1)
                {
                    masItog[5, i] = 0;
                }
                else
                {
                    masItog[5, i] = 1;
                }
                ///66666666
                if (masItog[5, i] == 0)
                {

                    masItog[6, i] = 0;
                }
                else
                {

                    masItog[6, i] = masItog[4, nom];
                    nom++;
                }
                /////////////777777
                if (masItog[6, i] == 0)
                {

                    masItog[7, i] = 0;
                }
                else
                {
                    for (int k = 0; k < (masTimeInteval.Length) / 5; k++)
                    {
                        if (masItog[6,i] > masTimeInteval[3, k] && masItog[6, i] < masTimeInteval[4, k])
                        {
                            masItog[7,i]= masTimeInteval[0,k];
                           
                        }

                    }
                    
                }
                ////////////////888888888888
                if (masItog[7, i] == 0)
                {

                    masItog[8, i] = 0;
                }
                else
                {
                    masItog[8, i] = masItog[3, i];
                    masItog[9, i] = masItog[8, i] + masItog[7, i];

                }



            }


            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    dgvItog[j, i].Value = masItog[j , i].ToString("0.##");
                }

            }

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            //interval(Convert.ToInt32(txt1.Text), masDistribution,lbl1);
        }

        private int interval( int b, double[,] mas, int a)
        {
            
            
            for (int i = 0; i < (mas.Length)/5; i++)
            {
                if (b> mas[3, i] && b < mas[4, i])
                {
                    a= i;
                    break;
                }

            }
            return a;
        }

            private void btnYes_Click(object sender, EventArgs e)
        {

            masYes = new double[5, 3];


            for (int i = 0; i < 3; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dgvYes.Rows.Add(dr);
            }

            masYes[0, 0] = 1;
            masYes[0, 1] = 0;

            masYes[1, 0] = Convert.ToDouble(txtYes.Text)/100;

            masYes[1, 1] = 1 - masYes[1, 0];




            for (int i = 0; i < 2; i++)
            {
              

                if (i == 0)
                {
                    masYes[2, i] = masYes[1, i];

                }
                else
                {
                    masYes[2, i] = masYes[1, i] + masYes[2, i - 1];
                }

                //интервал до от
                if (i == 0)
                {
                    masYes[3, i] = i;
                    masYes[4, i] = masYes[2, i] * 100;

                }
                else
                {
                    masYes[3, i] = masYes[4, i - 1] + 1;
                    masYes[4, i] = Math.Truncate(masYes[2, i] * 100);
                }

               






            }








            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    dgvYes[j, i].Value = masYes[j, i].ToString("0.##");
                }

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            dgvInterval.Rows.Clear();
            for (int i = 0; i < 2; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dgvInterval.Rows.Add(dr);
            }

            dgvTime.Rows.Clear();
            for (int i = 0; i < 2; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dgvTime.Rows.Add(dr);
            }


        }

        private void btnTimeInterval_Click(object sender, EventArgs e)
        {
            masTimeInteval = new double[5, 3];


            for (int i = 0; i < 3; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dgvTimeInetval.Rows.Add(dr);
            }



            masTimeInteval[0, 0] = 2;
            masTimeInteval[0, 1] = 4;
            masTimeInteval[0, 2] = 6;

            for (int i = 0; i < 3; i++)
            {
                
                masTimeInteval[1, i] = masTime[i, 1];

                if (i == 0)
                {
                    masTimeInteval[2, i] = masTime[i, 1];

                }
                else
                {
                    masTimeInteval[2, i] = masTime[i, 1] + masTimeInteval[2, i - 1];
                }

                //интервал до от
                if (i == 0)
                {
                    masTimeInteval[3, i] = i;
                    masTimeInteval[4, i] = masTimeInteval[2, i] * 100;

                }
                else
                {
                    masTimeInteval[3, i] = masTimeInteval[4, i - 1] + 1;
                    masTimeInteval[4, i] = Math.Truncate(masTimeInteval[2, i] * 100);
                }

                lstTimeInteval.Items.Add(masTimeInteval[2, i]);



                


            }
                 for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        dgvTimeInetval[j, i].Value = masTimeInteval[j, i].ToString("0.##");
                    }

                }




        }
        private void btnInterval_Click(object sender, EventArgs e)
        {
            masInterval = new double[6, 2];

            dgvInterval[0, 0].Value = "Число появлений";
            dgvInterval[0, 1].Value = "Вероя";


            masInterval[0, 0] = 25;
            masInterval[1, 0] = 35;
            masInterval[2, 0] = 18;

            masInterval[3, 0] = 10;

            masInterval[4, 0] = 8;
            masInterval[5, 0] = 4;

            for (int i = 0; i < 6; i++) sum1 += masInterval[i, 0];
            dgvInterval[7, 0].Value = sum1;
            for (int i = 0; i < 6; i++) masInterval[i, 1] = masInterval[i, 0] / sum1;
            for (int i = 0; i < 6; i++) sum2 += masInterval[i, 1];
            dgvInterval[7, 1].Value = sum2;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < 7; j++)
                {
                    dgvInterval[j, i].Value = masInterval[j - 1, i].ToString("0.##");
                }

            }

        }


        private void btnTime_Click(object sender, EventArgs e)
        {
            sumtime1 = 0;
            sumtime2 = 0;
            masTime = new double[3, 2];
            dgvTime[0, 0].Value = "Колличество интервью F";
            dgvTime[0, 1].Value = "Вероятность";
            for (int i = 0; i < lstT1.Items.Count; i++)
            {
                masTime[i, 0] = Convert.ToDouble(lstT1.Items[i]);
            }



            for (int i = 0; i < 3; i++) sumtime1 += masTime[i, 0];
            dgvTime[4, 0].Value = sumtime1;
            for (int i = 0; i < 3; i++) masTime[i, 1] = masTime[i, 0] / sumtime1;
            for (int i = 0; i < 3; i++)
            {
                sumtime2 += masTime[i, 1];
                lstT2.Items.Add(masTime[i, 1]);
            }
            dgvTime[4, 1].Value = sumtime2;



            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    dgvTime[j, i].Value = masTime[j - 1, i].ToString("0.##");
                }

            }


        }

        private void btnDistribiution_Click(object sender, EventArgs e)
        {
            masDistribution = new double[5, 6];
            dgvDistribiution.Rows.Clear();
            for (int i = 0; i < 6; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dgvDistribiution.Rows.Add(dr);
            }
            for (int i = 0; i < 6; i++)
            {
                masDistribution[0, i] = masInterval[i, 0]; ;
                masDistribution[1, i] = masInterval[i, 1];

                if (i == 0)
                {
                    masDistribution[2, i] = masDistribution[1, i];

                }
                else
                {
                    masDistribution[2, i] = masDistribution[1, i] + masDistribution[2, i - 1];
                }

                //интервал до от
                if (i == 0)
                {
                    masDistribution[3, i] = i;
                    masDistribution[4, i] = masDistribution[2, i] * 100;

                }
                else
                {
                    masDistribution[3, i] = masDistribution[4, i - 1] + 1;
                    masDistribution[4, i] = Math.Truncate(masDistribution[2, i] * 100);
                }




            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    dgvDistribiution[j, i].Value = masDistribution[j, i].ToString("0.##");
                }

            }


        }


    }


}