﻿using System;
using System.Windows.Forms;
using System.Threading;

namespace Dungeons
{
    public partial class Menu : Form
    {
        ScreenBeforeTheGame screenBeforeTheGame = new ScreenBeforeTheGame();
        byte runApp = 0;

        public Menu()
        {
            InitializeComponent();
            CenterToScreen();
            title.BackColor = System.Drawing.Color.Transparent;
            title.Parent = pictureBox1;
        }

        private void quitGame_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            if (startGame.Text == "START GAME (S)")
                PrepareToGame();
            else if (startGame.Text == "CONTINUE (C)")
                PrepareToGame();
            else
                Application.Restart();
        }

        private void Menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E)
                Exit();
            else if (e.KeyCode == Keys.I)
                InstructionApp();

            if(startGame.Text == "START GAME (S)")
            {
                if (e.KeyCode == Keys.S)
                    PrepareToGame();
            }
            else if(startGame.Text == "CONTINUE (C)")
            {
                if(e.KeyCode == Keys.C)
                    PrepareToGame();
            }
            else
            {
                if (e.KeyCode == Keys.R)
                    Application.Restart();
            }
        }

        private void PrepareToGame()
        {
            if (runApp == 0)
            {
                screenBeforeTheGame.Show();
                Thread.Sleep(1200);
                screenBeforeTheGame.Close();
                runApp++;
                Continue();
            }
            this.Close();     
        }

        private void Continue()
        {
            startGame.Text = "CONTINUE (C)";
        }

        private void Exit()
        {
            if (MessageBox.Show("Are you sure?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                == DialogResult.Yes)
            {
                Thread.Sleep(500);
                Application.Exit();
            }
        }

        public void Restart()
        {
            startGame.Text = "RESTART (R)";
        }

        private void instructionButton_Click(object sender, EventArgs e)
        {
            InstructionApp();
        }

        private void InstructionApp()
        {
            MessageBox.Show("In game:\n  M - Go to the menu\n  V  - Show/hide statistics\nIn splash screen:\n  S - Start game\n" +
                "  I - Instruction\n  E - Exit\nIn final screen:\n  SPACE - Skip screen", "Shortcuts",
                MessageBoxButtons.OK, MessageBoxIcon.Information);        
        }
    }
}
