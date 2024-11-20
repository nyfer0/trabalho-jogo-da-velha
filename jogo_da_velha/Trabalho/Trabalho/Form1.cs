using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho
{
    public partial class Form1 : Form
    {
        static int vitoriasPlayer = 0;
        static int vitoriasCPU = 0;
        static int quantidadeempates = 0;
        static int quantidadeRodadas = 1;
        static int EspacoVazio = 9;
        static bool turno = true;
        static bool jogo_final = false;
        static string[] texto = new string[9];
        private int tag1;

        public Form1()
        {
            InitializeComponent();
        }

        private void lblRodadas_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizaLabel();
        }

        private void AtualizaLabel()
        {
            lblPlayer.Text = string.Format("Player | Vitórias: {0}", vitoriasPlayer);
            lblCpu.Text = string.Format("CPU | Vitórias: {0}", vitoriasCPU);
            lblEmpates.Text = string.Format("Empates: {0}", quantidadeempates);
            lblRodadas.Text = string.Format("Rodadas: {0}", quantidadeRodadas);
        }

        private void pb_Click(object sender, EventArgs e)
        {
            var pb0 = (PictureBox)sender;
            var tag = int.Parse(pb0.Tag.ToString());
            if (pb0.Image == null && !jogo_final)
            {
                if (turno) 
                {
                    pb0.Image = Image.FromFile(@"..\..\images\x_bones.png");
                    pb0.SizeMode = PictureBoxSizeMode.StretchImage;

                    int pbTag = tag;
                    texto[tag] = "X";
                    EspacoVazio--;
                    turno = !turno;
                    Checagem(1);

                    if (!jogo_final)
                    {
                        CPUTurno();
                    }
                }                
            }
        }
        
        private void CPUTurno()
        {
            if (!turno)
            {
                Random escolhaRandom = new Random();
                int escolhaCPU = escolhaRandom.Next(0, 9);
                while (texto[escolhaCPU] != null)
                {
                    escolhaCPU = escolhaRandom.Next(0, 9);
                }
                PictureBox selectedPb = (PictureBox)this.Controls.Find("pb" + escolhaCPU, true)[0];
                selectedPb.Image = Image.FromFile(@"..\..\images\circle_skull.png");
                selectedPb.SizeMode = PictureBoxSizeMode.StretchImage;

                texto[escolhaCPU] = "O";
                EspacoVazio--;
                turno = !turno;
                Checagem(2);
            }
        }

        private void LimparTela()
        {
            for (int i = 0; i < 9; i++)
            {
                PictureBox pb = (PictureBox)this.Controls.Find("pb" + i, true)[0];
                pb.Image = null;
                
            }
            quantidadeRodadas++;
            AtualizaLabel();
            texto = new string[9];
            EspacoVazio = 9;
            jogo_final = false;
            turno = true;
        }

        private void Vencedor(int Ganhador) 
        {
            jogo_final = true;

            if (Ganhador == 1)
            {
                vitoriasPlayer++;
                MessageBox.Show("Parabéns Player você venceu");
            }
            else
            {
                vitoriasCPU++;
                MessageBox.Show("Infelizmente você perdeu");
            }
            LimparTela();
        }

        private void Checagem(int ChecagemPlayer)
        {
            string suporte;

            if (ChecagemPlayer == 1)
            {
                suporte = "X";
            }
            else
            {
                suporte = "O";
            }

            for (int horizontal = 0; horizontal < 9; horizontal += 3) // horizontal
            {
                if (suporte == texto[horizontal])
                {
                    if (texto[horizontal] == texto[horizontal + 1] && texto[horizontal] == texto[horizontal + 2])
                    {
                        Vencedor(ChecagemPlayer);                       
                        return;
                    }
                }
            }

            for (int vertical = 0; vertical < 3; vertical++) // vertical
            {
                if (suporte == texto[vertical])
                {
                    if (texto[vertical] == texto[vertical + 3] && texto[vertical] == texto[vertical + 6])
                    {
                        Vencedor(ChecagemPlayer);
                        return;
                    }
                }
            }

            if (texto[0] == suporte) // diagonal
            {
                if (texto[0] == texto[4] && texto[0] == texto[8])
                {
                    Vencedor(ChecagemPlayer);
                    return;
                }
            }

            if (texto[2] == suporte)
            {
                if (texto[2] == texto[4] && texto[2] == texto[6])
                {
                    Vencedor(ChecagemPlayer);
                    return;
                }
            }

            if (EspacoVazio == 0 && !jogo_final)
            {
                quantidadeempates++;
                MessageBox.Show("Empate");
                jogo_final = true;
                LimparTela();
                return;
            }
            
            
        }
    }
}
