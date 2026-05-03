using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
            _player = new Player(100, 0, 1, 10, 10);
            MoveTo(World.LocationByID(World.Location_ID_Home));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.Item_ID_Rusty_Sword), 1));


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
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
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

                        foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompeltionItems)
                        {
                            bool FoundItemInPlayersInventory = false;

                            foreach (InventoryItem II in _player.Inventory)
                            {
                                if (II.Details.ID == qci.Details.ID)
                                {
                                    FoundItemInPlayersInventory = true;

                                    if (II.Quantity < qci.Quantity)
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
                        if (PlayerHasAllItemToCompleteQuest)
                        {
                            rtbMessage.Text += Environment.NewLine;
                            rtbMessage.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;
                        }
                        foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompeltionItems)
                        {
                            foreach (InventoryItem II in _player.Inventory)
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

                        bool AddedItemToPlayerInventory = false;

                        foreach (InventoryItem II in _player.Inventory)
                        {
                            if (II.Details.ID == newLocation.QuestAvailableHere.RewardItem.ID)
                            {
                                II.Quantity++;

                                AddedItemToPlayerInventory = false;

                                break;
                            }
                        }

                        if (!AddedItemToPlayerInventory)
                        {
                            _player.Inventory.Add(new InventoryItem(newLocation.QuestAvailableHere.RewardItem, 1));
                        }

                        foreach (PlayerQuest pq in _player.PlayerQuests)
                        {
                            if (pq.Details.ID == newLocation.QuestAvailableHere.ID)
                            {
                                pq.IsCompleted = true;

                                break;

                            }
                        }


                    }
                }
                else
                {
                    rtbMessage.Text += "You Recieve The" + newLocation.QuestAvailableHere.Name + "Quest" + Environment.NewLine;
                    rtbMessage.Text += newLocation.QuestAvailableHere.Description;
                    rtbMessage.Text += "To complete it return with: " + Environment.NewLine;

                    foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompeltionItems)
                    {
                        if (qci.Quantity == 1)
                        {
                            rtbMessage.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {

                            rtbMessage.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessage.Text = Environment.NewLine;
                }
            }

            if (newLocation.MonsterLivingHere != null)
            {
                rtbMessage.Text += "There's a:" + newLocation.MonsterLivingHere.Name + "here";

                Monster StanderedMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);

                _currentmonster = new Monster(StanderedMonster.ID, StanderedMonster.Name, StanderedMonster.MaxDamage, StanderedMonster.RewardedXP, StanderedMonster.RewardedGold, StanderedMonster.CurrentHP, StanderedMonster.MaxHP);

                foreach (LootItem li in StanderedMonster.LootTable)
                {
                    _currentmonster.LootTable.Add(li);
                }

                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                _currentmonster = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;


            }

            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";


            dgvInventory.Rows.Clear();

            foreach (InventoryItem ii in _player.Inventory)
            {
                if (ii.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { ii.Details.Name, ii.Quantity.ToString() });
                }

            }

            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest pq in _player.PlayerQuests)
            {
                dgvQuests.Rows.Add(new[] { pq.Details.Name, pq.IsCompleted.ToString() });
            }

            List<Weapon> weapon = new List<Weapon>();

            foreach (InventoryItem ii in _player.Inventory)
            {
                if (ii.Details is Weapon)
                {
                    if (ii.Quantity > 0)
                    {
                        weapon.Add((Weapon)ii.Details);
                    }
                }
            }
            if (weapon.Count == 0)
            {
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapon;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;
            }

            List<HealingPotion> healingpotion = new List<HealingPotion>();

            foreach (InventoryItem ii in _player.Inventory)
            {
                if (ii.Details is HealingPotion)
                {
                    if (ii.Quantity > 0)
                    {
                        healingpotion.Add((HealingPotion)ii.Details);
                    }
                }
            }

            if (healingpotion.Count == 0)
            {
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingpotion;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }



        }
    }
}


