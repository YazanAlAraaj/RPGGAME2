using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RPGGAME2
{
    public partial class rpggame1 : Form
    {

        private Player _player;
        private Monster _currentmonster;

        public rpggame1()
        {
            InitializeComponent();
            _player = new Player(100, 0, 1, 100, 100);
            MoveTo(World.Location_ID_Home);


            lblHitPoints.Text = _player.CurrentHP.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblLevel.Text = _player.Level.ToString();
            lblExperience.Text = _player.Experience.ToString();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblExperience_Click(object sender, EventArgs e)
        {

        }

        private void lblHitPoints_Click(object sender, EventArgs e)
        {

        }

        private void lblEquipment_Click(object sender, EventArgs e)
        {

        }

        private void cboWeapons1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void cboPotions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnUsePotion_Click(object sender, EventArgs e)
        {

        }


        private void btnUseWeapon_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnEast_Click(object sender, EventArgs e)
        {

        }

        private void btnWest_Click(object sender, EventArgs e)
        {

        }

        private void btnSouth_Click(object sender, EventArgs e)
        {

        }

        private void MoveTo(Location newLocation)
        {
            if (newLocation.ItemRequiredToEnter != null)
            {
                bool PlayerHasRequiredItem = false;

                foreach (InventoryItem item in _player.Inventory)
                {
                    PlayerHasRequiredItem = true;
                    break;
                }
                if (!PlayerHasRequiredItem)
                {
                    rtbMessage.Text += "You must have a " + newLocation.ItemRequiredToEnter.Name + " to enter this location." + Environment.NewLine;
                    return;
                }
            }
            _player.CurrentLocation = newLocation;

            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            _player.CurrentHP = _player.MaxHP;

            lblHitPoints.Text = _player.CurrentHP.ToString();

            if (newLocation.QuestAvailableHere != null)
            {
                bool PlayerAlreadyHasQuest = false;
                bool PlayerAlreadyCompletedQuest = false;

                foreach (PlayerQuest playerquest in _player.PlayerQuests)
                {
                    if (playerquest.Details.ID == newLocation.QuestAvailableHere.ID)
                    {
                        PlayerAlreadyHasQuest = true;

                        if (playerquest.IsCompleted)
                        {
                            PlayerAlreadyCompletedQuest = true;
                        }
                    }
                }
                if (PlayerAlreadyHasQuest)
                {
                    if (!PlayerAlreadyCompletedQuest)
                    {
                        bool PlayerHasAllItemToCompleteQuest = true;

                        foreach(QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompeltionItems)
                        {
                            bool FoundItemInPlayersInventory=false;
                            
                            foreach(InventoryItem II in _player.Inventory)
                            {
                                if (II.Details.ID == qci.Details.ID)
                                {
                                    FoundItemInPlayersInventory = true;

                                    if(II.Quantity < qci.Quantity)
                                    {
                                        PlayerHasAllItemToCompleteQuest = false;
                                        break;
                                    }
                                    break;
                                }
                            }
                            if (!FoundItemInPlayersInventory)
                            {
                                PlayerHasAllItemToCompleteQuest = false;

                                break;
                            }
                        }
                        if(PlayerHasAllItemToCompleteQuest)
                        {
                            rtbMessage.Text += Environment.NewLine;
                            rtbMessage.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;
                        }
                        foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompeltionItems)
                        {
                            foreach(InventoryItem II in _player.Inventory)
                            {
                                if (II.Details.ID == qci.Details.ID)
                                {
                                    II.Quantity -= qci.Quantity;

                                    break;
                                }
                            }
                        }
                        rtbMessage.Text += "You receive: " + Environment.NewLine;
                        rtbMessage.Text += newLocation.QuestAvailableHere.RewardedXP.ToString() + " experience points" + Environment.NewLine;
                        rtbMessage.Text += newLocation.QuestAvailableHere.RewardedGold.ToString() + " gold" + Environment.NewLine;
                        rtbMessage.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                        rtbMessage.Text += Environment.NewLine;

                        _player.Experience += newLocation.QuestAvailableHere.RewardedXP;
                        _player.Gold += newLocation.QuestAvailableHere.RewardedGold;



                    }
                }

            }




        }
    }
}

