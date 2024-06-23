using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RecipeCostCalculator
{
    public class Ingredient
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }

    public partial class MainForm : Form
    {
        private List<Ingredient> ingredients = new List<Ingredient>();

        private TextBox txtIngredientName;
        private TextBox txtIngredientCost;
        private Button btnAddIngredient;
        private ListBox lstIngredients;
        private Button btnCalculateTotal;
        private Label lblTotalCost;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtIngredientName = new TextBox();
            this.txtIngredientName.Location = new System.Drawing.Point(20, 20);
            this.txtIngredientName.Size = new System.Drawing.Size(150, 20);

            this.txtIngredientCost = new TextBox();
            this.txtIngredientCost.Location = new System.Drawing.Point(200, 20);
            this.txtIngredientCost.Size = new System.Drawing.Size(80, 20);

            this.btnAddIngredient = new Button();
            this.btnAddIngredient.Text = "Add Ingredient";
            this.btnAddIngredient.Location = new System.Drawing.Point(20, 60);
            this.btnAddIngredient.Click += btnAddIngredient_Click;

            this.lstIngredients = new ListBox();
            this.lstIngredients.Location = new System.Drawing.Point(20, 100);
            this.lstIngredients.Size = new System.Drawing.Size(260, 120);

            this.btnCalculateTotal = new Button();
            this.btnCalculateTotal.Text = "Calculate Total Cost";
            this.btnCalculateTotal.Location = new System.Drawing.Point(20, 240);
            this.btnCalculateTotal.Click += btnCalculateTotal_Click;

            this.lblTotalCost = new Label();
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Location = new System.Drawing.Point(20, 280);

            this.ClientSize = new System.Drawing.Size(300, 320);
            this.Controls.Add(this.txtIngredientName);
            this.Controls.Add(this.txtIngredientCost);
            this.Controls.Add(this.btnAddIngredient);
            this.Controls.Add(this.lstIngredients);
            this.Controls.Add(this.btnCalculateTotal);
            this.Controls.Add(this.lblTotalCost);
            this.Text = "Recipe Cost Calculator";
        }

        private void btnAddIngredient_Click(object sender, EventArgs e)
        {
            string ingredientName = txtIngredientName.Text.Trim();
            if (string.IsNullOrEmpty(ingredientName))
            {
                MessageBox.Show("Please enter ingredient name.");
                return;
            }

            decimal ingredientCost;
            if (!decimal.TryParse(txtIngredientCost.Text, out ingredientCost))
            {
                MessageBox.Show("Invalid ingredient cost. Please enter a valid number.");
                return;
            }

            Ingredient newIngredient = new Ingredient
            {
                Name = ingredientName,
                Cost = ingredientCost
            };

            ingredients.Add(newIngredient);
            UpdateIngredientsList();
            ClearIngredientFields();
        }

        private void UpdateIngredientsList()
        {
            lstIngredients.Items.Clear();
            foreach (Ingredient ingredient in ingredients)
            {
                lstIngredients.Items.Add($"{ingredient.Name}: ${ingredient.Cost}");
            }
        }

        private void ClearIngredientFields()
        {
            txtIngredientName.Text = "";
            txtIngredientCost.Text = "";
        }

        private void btnCalculateTotal_Click(object sender, EventArgs e)
        {
            decimal totalCost = 0;
            foreach (Ingredient ingredient in ingredients)
            {
                totalCost += ingredient.Cost;
            }

            lblTotalCost.Text = $"Total Cost: ${totalCost}";
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
