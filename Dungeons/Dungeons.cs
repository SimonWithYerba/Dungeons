﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dungeons
{
    public partial class Dungeons : Form
    {
        private Game game;
        private Random random = new Random();

        public Dungeons()
        {
            InitializeComponent();
            player30.Visible = true;
            CenterToScreen();
        }

        private void Dungeons_Load(object sender, EventArgs e)
        {
            //70(left), 50(up), 420(right), 139(bottom)
            //78, 57, 420, 155
            game = new Game(new Rectangle(70, 50, 420, 145));
            game.NewLevel(random);
            UpdateCharacters();
        }

        private void UpdateCharacters()
        {
            int enemiesShown;
            UpdatePlayer();
            enemiesShown = UpdateEnemies();
           
            setTheVisibilityOfTheWeaponOnTheBoard();

            Control weaponControl = null;
            chooseWeaponToDisplay(weaponControl);

            setClearEquipWeapon();
            setNoneWeaponInInventory();
            checkInventory();
            choosenWeapon();

            checkTheStatusOfWeaponOnTheBoard(chooseWeaponToDisplay(weaponControl));

            checkHitPoints(game.PlayerHitPoints);
            countEnemies(enemiesShown);               
        }

        private int UpdateEnemies()
        {
            bool showBat = false;
            bool showGhost = false;
            int enemiesShown = 0;

            foreach (Enemy enemy in game.Enemies)
            {
                if (enemy is Bat)
                {
                    bat30.Location = enemy.Location;
                    batHitPoints.Text = enemy.HitPoints.ToString();
                    if (enemy.HitPoints >= 1)
                    {
                        showBat = true;
                        showEnemies(showBat);
                        enemiesShown++;
                    }
                }
                if(enemy is Ghost)
                {
                    ghost30.Location = enemy.Location;
                    ghostHitPoints.Text = enemy.HitPoints.ToString();
                    if(enemy.HitPoints >= 1)
                    {
                        showGhost = true;
                        showEnemies(showGhost);
                        enemiesShown++;
                    }
                }
            }
            return enemiesShown;
        }

        private void showEnemies(bool showEnemy)
        {
            foreach(Enemy enemy in game.Enemies)
            {
                if(enemy is Bat)
                {
                    if (showEnemy)
                        bat30.Visible = true;
                    else
                    {
                        bat30.Visible = false;
                        batHitPoints.Text = "";
                    }
                }
                if(enemy is Ghost)
                {
                    if (showEnemy)
                        ghost30.Visible = true;
                    else
                    {
                        ghost30.Visible = false;
                        ghostHitPoints.Text = "";
                    }
                }
            }
        }

        private void UpdatePlayer()
        {
            player30.Location = game.PlayerLocation;
            playerHitPoints.Text = game.PlayerHitPoints.ToString();
        }

        private void checkTheStatusOfWeaponOnTheBoard(Control weaponControl)
        {
            weaponControl.Location = game.WeaponInRoom.Location;
            if (game.WeaponInRoom.PickedUp)
                weaponControl.Visible = false;
            else
                weaponControl.Visible = true;
        }

        private Control chooseWeaponToDisplay(Control weaponControl)
        {
            switch (game.WeaponInRoom.Name)
            {
                case "Sword":
                    {
                        weaponControl = sword30;
                        break;
                    }
                case "Bow":
                    {
                        weaponControl = bow30;
                        break;
                    }
                case "Mace":
                    {
                        weaponControl = mace30;
                        break;
                    }
            }
            weaponControl.Visible = true;
            return weaponControl;
        }

        private void countEnemies(int enemiesShown)
        {
            if (enemiesShown < 1)
            {
                MessageBox.Show("You defeated all of enemies at this level", "Great Job");
                game.NewLevel(random);
                UpdateCharacters();
            }
        }

        private void checkHitPoints(int playerHitPoints)
        {
            if (game.PlayerHitPoints <= 0)
            {
                MessageBox.Show("You have been killed");
                Application.Exit();
            }
        }

        private void setTheVisibilityOfTheWeaponOnTheBoard()
        {
            sword30.Visible = false;
            bow30.Visible = false;
            mace30.Visible = false;
        }

        private void setNoneWeaponInInventory()
        {
            equipSword.BorderStyle = BorderStyle.None;
            equipBow.BorderStyle = BorderStyle.None;
            equipMace.BorderStyle = BorderStyle.None;
        }

        private void setClearEquipWeapon()
        {
            equipWeaponSword.Visible = false;
            equipWeaponBow.Visible = false;
            equipWeaponMace.Visible = false;
        }

        private void choosenWeapon()
        {
            if(game.choosenWeaponByPlayer() == "Sword")
            {
                equipSword.BorderStyle = BorderStyle.FixedSingle;
                equipWeaponSword.Visible = true;
            }
            else if(game.choosenWeaponByPlayer() == "Bow")
            {
                equipBow.BorderStyle = BorderStyle.FixedSingle;
                equipWeaponBow.Visible = true;
            }
            else if(game.choosenWeaponByPlayer() == "Mace")
            {
                equipMace.BorderStyle = BorderStyle.FixedSingle;
                equipWeaponMace.Visible = true;
            }
        }

        private void checkInventory()
        {
            if (game.CheckPlayerInventory("Sword"))
                equipSword.Visible = true;
            if (game.CheckPlayerInventory("Bow"))
                equipBow.Visible = true;
            if (game.CheckPlayerInventory("Mace"))
                equipMace.Visible = true;
        }

        private void moveLeft_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Left, random);
            UpdateCharacters();
        }

        private void moveRight_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Right, random);
            UpdateCharacters();
        }

        private void moveUp_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Up, random);
            UpdateCharacters();
        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Down, random);
            UpdateCharacters();
        }

        private void attackLeft_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Left, random);
            UpdateCharacters();
        }

        private void attackRight_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Right, random);
            UpdateCharacters();
        }

        private void attackUp_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Up, random);
            UpdateCharacters();
        }

        private void attackDown_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);
            UpdateCharacters();
        }

        private void equipSword_Click(object sender, EventArgs e)
        {
            game.Equip("Sword");
            UpdateCharacters();
        }

        private void equipBow_Click(object sender, EventArgs e)
        {
            game.Equip("Bow");
            UpdateCharacters();
        }

        private void equipMace_Click(object sender, EventArgs e)
        {
            game.Equip("Mace");
            UpdateCharacters();
        }
    }
}
