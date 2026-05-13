using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
            _player = new Player(10, 0, 1, 10, 10);
            MoveTo(World.LocationByID(World.Location_ID_Home));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.Item_ID_Rusty_Sword), 1));


            lblHitPoints.Text = _player.CurrentHP.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblLevel.Text = _player.Level.ToString();
            lblExperience.Text = _player.Experience.ToString();


        }
        private void btnNorth_Click(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void MoveTo(Location newLocation)
        {
            if (!_player.HasRequiredItemToEnterThisLocation(newLocation))
            {
                rtbMessage.Text += "You must have" + newLocation.ItemRequiredToEnter.Name + "to enter this location" + Environment.NewLine;
            }

            _player.CurrentLocation = newLocation;

            btnNorth.Visible = newLocation.LocationToNorth != null;
            btnEast.Visible = newLocation.LocationToEast != null;
            btnSouth.Visible = newLocation.LocationToSouth != null;
            btnWest.Visible = newLocation.LocationToWest != null;

            rtbLocation.Text += newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            _player.CurrentHP = _player.MaxHP;

            lblHitPoints.Text = _player.CurrentHP.ToString();

     

            if (newLocation.QuestAvailableHere != null)
            {

                _player.PlayerQuests.Add(new PlayerQuest(newLocation.QuestAvailableHere,false));
                bool PlayerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool PlayerAlreadyCompletedQuest = _player.HasCompletedThisQuest(newLocation.QuestAvailableHere);

                if (PlayerAlreadyHasQuest)
                {
                    if (!PlayerAlreadyCompletedQuest)
                    {
                        bool PlayerHasAllItemToCompleteQuest = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);
                        if (PlayerHasAllItemToCompleteQuest)
                        {
                            rtbMessage.Text += Environment.NewLine;
                            rtbMessage.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;
                        }

                        _player.RemoveQuestCompletionItem(newLocation.QuestAvailableHere);

                        rtbMessage.Text += "You receive: " + Environment.NewLine;
                        rtbMessage.Text += newLocation.QuestAvailableHere.RewardedXP.ToString() + " experience points" + Environment.NewLine;
                        rtbMessage.Text += newLocation.QuestAvailableHere.RewardedGold.ToString() + " gold" + Environment.NewLine;
                        rtbMessage.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                        rtbMessage.Text += Environment.NewLine;

                        _player.Experience += newLocation.QuestAvailableHere.RewardedXP;
                        _player.Gold += newLocation.QuestAvailableHere.RewardedGold;

                        _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);


                        _player.MarkThisQuestCompleted(newLocation.QuestAvailableHere);



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
                    rtbMessage.Text += Environment.NewLine;
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
            UpdateInventoryListInUI();

            UpdateQuestsListInUI();

            UpdateWeaponListInUI();

            UpdatePotionListInUI();

        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            HealingPotion currenthealingpotion = (HealingPotion)cboPotions.SelectedItem;
            Random random = new Random();

            _player.CurrentHP = (_player.CurrentHP + currenthealingpotion.AmountToHeal);

            if (_player.CurrentHP > _player.MaxHP)
            {
                _player.CurrentHP = _player.MaxHP;
            }
            foreach (InventoryItem ii in _player.Inventory)
            {
                if (ii.Details.ID == currenthealingpotion.ID)
                {
                    ii.Quantity--;
                    break;
                }
            }
            rtbMessage.Text += "You drink a " + currenthealingpotion.Name + Environment.NewLine;

            int damageToPlayer = random.Next(0, _currentmonster.MaxDamage);

            rtbMessage.Text += "The " + _currentmonster.Name + " did " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;

            _player.CurrentHP -= damageToPlayer;

            if (_player.MaxHP <= 0)
            {
                rtbMessage.Text += "The " + _currentmonster.Name + " killed you." + Environment.NewLine;
                MoveTo(World.LocationByID(World.Location_ID_Home));
            }
            lblHitPoints.Text += _player.CurrentHP.ToString();

            UpdateInventoryListInUI();

            UpdatePotionListInUI();
        }


        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;
            Random random = new Random();

            int DamageToMonster = random.Next(currentWeapon.MinDamage, currentWeapon.MaxDamage);

            _currentmonster.CurrentHP -= DamageToMonster;

            rtbMessage.Text += "You hit" + _currentmonster.Name + "for" + DamageToMonster.ToString() + "Points" + Environment.NewLine;

            if (_currentmonster.CurrentHP <= 0)
            {
                rtbMessage.Text += Environment.NewLine;
                rtbMessage.Text += "You killed" + _currentmonster.Name + Environment.NewLine;

                _player.Experience += _currentmonster.RewardedXP;
                rtbMessage.Text += "You recieve" + _currentmonster.RewardedXP.ToString() + Environment.NewLine;

                _player.Gold += _currentmonster.RewardedGold;
                rtbMessage.Text += "You recieve" + _currentmonster.RewardedGold.ToString() + Environment.NewLine;

                List<InventoryItem> LootedItems = new List<InventoryItem>();

                foreach (LootItem lootitem in _currentmonster.LootTable)
                {
                    if (random.Next(1, 100) <= lootitem.DropPercentage)
                    {
                        LootedItems.Add(new InventoryItem(lootitem.Details, 1));
                    }
                }
                if (LootedItems.Count == 0)
                {
                    foreach (LootItem lootitem in _currentmonster.LootTable)
                    {
                        if (lootitem.DefaultItem)
                        {
                            LootedItems.Add(new InventoryItem(lootitem.Details, 1));
                        }

                    }
                }
                foreach (InventoryItem ii in LootedItems)
                {
                    _player.AddItemToInventory(ii.Details);
                    if (ii.Quantity == 1)
                    {
                        rtbMessage.Text += "You loot " + ii.Quantity.ToString() + " " + ii.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessage.Text += "You loot " + ii.Quantity.ToString() + " " + ii.Details.NamePlural + Environment.NewLine;
                    }
                }

                lblHitPoints.Text = _player.CurrentHP.ToString();
                lblGold.Text = _player.Gold.ToString();
                lblExperience.Text = _player.Experience.ToString();
                lblLevel.Text = _player.Level.ToString();

                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdatePotionListInUI();

                rtbMessage.Text += Environment.NewLine;

                MoveTo(_player.CurrentLocation);

            }
            else
            {
                int DamageToPlayer = random.Next(0, _currentmonster.MaxDamage);

                rtbMessage.Text += Environment.NewLine;
                rtbMessage.Text = "The" + _currentmonster.Name + "Has Dealt" + _currentmonster.MaxDamage.ToString() + Environment.NewLine;

                _player.CurrentHP -= DamageToPlayer;

                lblHitPoints.Text = _player.CurrentHP.ToString();

                if (_player.CurrentHP <= 0)
                {
                    rtbMessage.Text += _currentmonster.Name + "Has Killed you" + Environment.NewLine;
                    MoveTo(World.LocationByID(World.Location_ID_Home));
                }
            }

        }


        private void UpdateInventoryListInUI()
        {
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
        }

        private void UpdateQuestsListInUI()
        {
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
        }

        private void UpdateWeaponListInUI()
        {
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
        }

        private void UpdatePotionListInUI()
        {
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

    }
}




