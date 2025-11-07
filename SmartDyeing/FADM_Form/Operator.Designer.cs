using System.Drawing;
using System.Windows.Forms;

namespace SmartDyeing.FADM_Form
{
    partial class Operator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Operator));
            this.btn_save = new System.Windows.Forms.Button();
            this.dgv_Operator = new System.Windows.Forms.DataGridView();
            this.rdo_2 = new System.Windows.Forms.RadioButton();
            this.rdo_1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Operator)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.Name = "btn_save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // dgv_Operator
            // 
            resources.ApplyResources(this.dgv_Operator, "dgv_Operator");
            this.dgv_Operator.AllowUserToResizeColumns = false;
            this.dgv_Operator.AllowUserToResizeRows = false;
            this.dgv_Operator.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_Operator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Operator.MultiSelect = false;
            this.dgv_Operator.Name = "dgv_Operator";
            this.dgv_Operator.RowHeadersVisible = false;
            this.dgv_Operator.RowTemplate.Height = 23;
            this.dgv_Operator.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // rdo_2
            // 
            resources.ApplyResources(this.rdo_2, "rdo_2");
            this.rdo_2.Name = "rdo_2";
            this.rdo_2.UseVisualStyleBackColor = true;
            this.rdo_2.Click += new System.EventHandler(this.rdo_2_Click);
            // 
            // rdo_1
            // 
            resources.ApplyResources(this.rdo_1, "rdo_1");
            this.rdo_1.Checked = true;
            this.rdo_1.Name = "rdo_1";
            this.rdo_1.TabStop = true;
            this.rdo_1.UseVisualStyleBackColor = true;
            this.rdo_1.Click += new System.EventHandler(this.rdo_1_Click);
            // 
            // Operator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdo_2);
            this.Controls.Add(this.rdo_1);
            this.Controls.Add(this.dgv_Operator);
            this.Controls.Add(this.btn_save);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Operator";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Operator_FormClosing);
            this.Load += new System.EventHandler(this.Operator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Operator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridView dgv_Operator;
        private RadioButton rdo_2;
        private RadioButton rdo_1;
    }
}