using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BnKTools
{
    public partial class MainForm : Form
    {
        private ListViewColumnSorter lvwColumnSorter;

        public MainForm()
        {
            InitializeComponent();
            // Create an instance of a ListView column sorter and assign it
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            this.listview_transaksi.ListViewItemSorter = lvwColumnSorter;

            FetchMainData();
            FetchListData();

            ResetMainData();
            ResetFilterData();
            ResetActionData();

            HideTab();
        }

        private void listview_transaksi_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listview_transaksi.Sort();
        }

        private void FetchMainData()
        {
            #region TODAY TOTALS
            double today_profit = DB_Transaksi.Read_Value("profit", false, DateTime.Today, DateTime.Today);
            lbl_today_profit.Text = today_profit.ToString("0.00") + "%";
            if (today_profit > 0) lbl_today_profit.ForeColor = Color.Green;
            else lbl_today_profit.ForeColor = Color.Black;

            double today_loss = DB_Transaksi.Read_Value("loss", false, DateTime.Today, DateTime.Today);
            lbl_today_loss.Text = today_loss.ToString("0.00") + "%";
            if (today_loss < 0) lbl_today_loss.ForeColor = Color.Red;
            else lbl_today_loss.ForeColor = Color.Black;

            double today_total = DB_Transaksi.Read_Value("total", false, DateTime.Today, DateTime.Today);
            lbl_today_total.Text = today_total.ToString("0.00") + "%";
            if (today_total > 0) lbl_today_total.ForeColor = Color.Green;
            else if (today_total < 0) lbl_today_total.ForeColor = Color.Red;
            else lbl_today_total.ForeColor = Color.Black;
            #endregion
            #region TODAY AVERAGES
            double today_avg_profit = DB_Transaksi.Read_Value("profit", true, DateTime.Today, DateTime.Today);
            lbl_today_avg_profit.Text = today_avg_profit.ToString("0.00") + "%";
            if (today_avg_profit > 0) lbl_today_avg_profit.ForeColor = Color.Green;
            else lbl_today_avg_profit.ForeColor = Color.Black;

            double today_avg_loss = DB_Transaksi.Read_Value("loss", true, DateTime.Today, DateTime.Today);
            lbl_today_avg_loss.Text = today_avg_loss.ToString("0.00") + "%";
            if (today_avg_loss < 0) lbl_today_avg_loss.ForeColor = Color.Red;
            else lbl_today_avg_loss.ForeColor = Color.Black;

            double today_total_avg = DB_Transaksi.Read_Value("total", true, DateTime.Today, DateTime.Today);
            lbl_today_avg_total.Text = today_total_avg.ToString("0.00") + "%";
            if (today_total_avg > 0) lbl_today_avg_total.ForeColor = Color.Green;
            else if (today_total_avg < 0) lbl_today_avg_total.ForeColor = Color.Red;
            else lbl_today_avg_total.ForeColor = Color.Black;
            #endregion
            var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            #region MONTHLY TOTALS
            double monthly_profit = DB_Transaksi.Read_Value("profit", false, firstDayOfMonth, lastDayOfMonth);
            lbl_monthly_profit.Text = monthly_profit.ToString("0.00") + "%";
            if (monthly_profit > 0) lbl_monthly_profit.ForeColor = Color.Green;
            else lbl_monthly_profit.ForeColor = Color.Black;

            double monthly_loss = DB_Transaksi.Read_Value("loss", false, firstDayOfMonth, lastDayOfMonth);
            lbl_monthly_loss.Text = monthly_loss.ToString("0.00") + "%";
            if (monthly_loss < 0) lbl_monthly_loss.ForeColor = Color.Red;
            else lbl_monthly_loss.ForeColor = Color.Black;

            double monthly_total = DB_Transaksi.Read_Value("average", false, firstDayOfMonth, lastDayOfMonth);
            lbl_monthly_total.Text = monthly_total.ToString("0.00") + "%";
            if (monthly_total > 0) lbl_monthly_total.ForeColor = Color.Green;
            else if (monthly_total < 0) lbl_monthly_total.ForeColor = Color.Red;
            else lbl_monthly_total.ForeColor = Color.Black;
            #endregion
            #region MONTHLY AVERAGES
            double monthly_avg_profit = DB_Transaksi.Read_Value("profit", true, firstDayOfMonth, lastDayOfMonth);
            lbl_monthly_avg_profit.Text = monthly_avg_profit.ToString("0.00") + "%";
            if (monthly_avg_profit > 0) lbl_monthly_avg_profit.ForeColor = Color.Green;
            else lbl_monthly_avg_profit.ForeColor = Color.Black;

            double monthly_avg_loss = DB_Transaksi.Read_Value("loss", true, firstDayOfMonth, lastDayOfMonth);
            lbl_monthly_avg_loss.Text = monthly_avg_loss.ToString("0.00") + "%";
            if (monthly_avg_loss < 0) lbl_monthly_avg_loss.ForeColor = Color.Red;
            else lbl_monthly_avg_loss.ForeColor = Color.Black;

            double monthly_total_avg = DB_Transaksi.Read_Value("average", true, firstDayOfMonth, lastDayOfMonth);
            lbl_monthly_avg_total.Text = monthly_total_avg.ToString("0.00") + "%";
            if (monthly_total_avg > 0) lbl_monthly_avg_total.ForeColor = Color.Green;
            else if (monthly_total_avg < 0) lbl_monthly_avg_total.ForeColor = Color.Red;
            else lbl_monthly_avg_total.ForeColor = Color.Black;
            #endregion
        }

        private void FetchMainCustomData()
        {
            #region CUSTOM TOTALS
            double custom_profit = DB_Transaksi.Read_Value("profit", false, dateTime_menu_start.Value, dateTime_menu_end.Value);
            lbl_custom_profit.Text = custom_profit.ToString("0.00") + "%";
            if (custom_profit > 0) lbl_custom_profit.ForeColor = Color.Green;
            else lbl_custom_profit.ForeColor = Color.Black;

            double custom_loss = DB_Transaksi.Read_Value("loss", false, dateTime_menu_start.Value, dateTime_menu_end.Value);
            lbl_custom_loss.Text = custom_loss.ToString("0.00") + "%";
            if (custom_loss < 0) lbl_custom_loss.ForeColor = Color.Red;
            else lbl_custom_loss.ForeColor = Color.Black;

            double custom_total = DB_Transaksi.Read_Value("average", false, dateTime_menu_start.Value, dateTime_menu_end.Value);
            lbl_custom_total.Text = custom_total.ToString("0.00") + "%";
            if (custom_total > 0) lbl_custom_total.ForeColor = Color.Green;
            else if (custom_total < 0) lbl_custom_total.ForeColor = Color.Red;
            else lbl_custom_total.ForeColor = Color.Black;
            #endregion
            #region CUSTOM AVERAGES
            double custom_avg_profit = DB_Transaksi.Read_Value("profit", true, dateTime_menu_start.Value, dateTime_menu_end.Value);
            lbl_custom_avg_profit.Text = custom_avg_profit.ToString("0.00") + "%";
            if (custom_avg_profit > 0) lbl_custom_avg_profit.ForeColor = Color.Green;
            else lbl_custom_avg_profit.ForeColor = Color.Black;

            double custom_avg_loss = DB_Transaksi.Read_Value("loss", true, dateTime_menu_start.Value, dateTime_menu_end.Value);
            lbl_custom_avg_loss.Text = custom_avg_loss.ToString("0.00") + "%";
            if (custom_avg_loss < 0) lbl_custom_avg_loss.ForeColor = Color.Red;
            else lbl_custom_avg_loss.ForeColor = Color.Black;

            double custom_total_avg = DB_Transaksi.Read_Value("average", true, dateTime_menu_start.Value, dateTime_menu_end.Value);
            lbl_custom_avg_total.Text = custom_total_avg.ToString("0.00") + "%";
            if (custom_total_avg > 0) lbl_custom_avg_total.ForeColor = Color.Green;
            else if (custom_total_avg < 0) lbl_custom_avg_total.ForeColor = Color.Red;
            else lbl_custom_avg_total.ForeColor = Color.Black;
            #endregion
        }

        private void ResetMainData()
        {
            dateTime_menu_start.Format = DateTimePickerFormat.Custom;
            dateTime_menu_start.CustomFormat = "dd/MM/yyyy";
            dateTime_menu_end.Format = DateTimePickerFormat.Custom;
            dateTime_menu_end.CustomFormat = "dd/MM/yyyy";
        }

        private void btn_filter_Click(object sender, EventArgs e)
        {
            if (cb_filter_date.Checked)
            {
                FetchQueryListData(tb_filter.Text, dateTime_filter.Value, dateTime_filter_2.Value);
            }
            else
            {
                if (tb_filter.Text == "")
                {
                    MessageBox.Show("Mohon masukkan keyword yang benar.");
                    return;
                }

                FetchQueryListData(tb_filter.Text, dateTime_filter.MinDate, dateTime_filter.MaxDate);
            }
        }
        private void cb_filter_date_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_filter_date.Checked)
            {
                dateTime_filter.Enabled = true;
                dateTime_filter_2.Enabled = true;
            }
            else
            {
                dateTime_filter.Enabled = false;
                dateTime_filter_2.Enabled = false;
            }
        }

        private void btn_resetfilter_Click(object sender, EventArgs e)
        {
            ResetFilterData();
            FetchListData();
        }

        private void ResetFilterData()
        {
            tb_filter.Text = "";
            cb_filter_date.Checked = true;
            dateTime_filter.Value = DateTime.Today;
            dateTime_filter_2.Value = DateTime.Today;
            dateTime_filter.Format = DateTimePickerFormat.Custom;
            dateTime_filter.CustomFormat = "dd/MM/yyyy";
            dateTime_filter_2.Format = DateTimePickerFormat.Custom;
            dateTime_filter_2.CustomFormat = "dd/MM/yyyy";
        }

        private void tb_kode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Mohon masukkan alphabet saja.");
                e.Handled = true;
                return;
            }

            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void tb_currency_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Mohon masukkan alphabet saja.");
                e.Handled = true;
                return;
            }

            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void tb_filter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Mohon masukkan alphabet saja.");
                e.Handled = true;
                return;
            }

            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            btn_aksi.Text = "Tambah";
            ResetActionData();
            group_basicinfo.Enabled = true;
            ShowTab();
        }

        private void btn_ubah_Click(object sender, EventArgs e)
        {
            if (listview_transaksi.SelectedIndices.Count > 0)
            {
                Transaksi transaksi = DB_Transaksi.Read_Transaksi(int.Parse(listview_transaksi.SelectedItems[0].Text));
                SetActionData(transaksi);

                btn_aksi.Text = "Ubah";
                group_basicinfo.Enabled = true;
                ShowTab();
            }
            else
            {
                MessageBox.Show("Mohon pilih data yang ingin diubah terlebih dahulu.");
            }
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            if (listview_transaksi.SelectedIndices.Count > 0)
            {
                Transaksi transaksi = DB_Transaksi.Read_Transaksi(int.Parse(listview_transaksi.SelectedItems[0].Text));
                SetActionData(transaksi);

                btn_aksi.Text = "Hapus";
                group_basicinfo.Enabled = false;
                ShowTab();
            }
            else
            {
                MessageBox.Show("Mohon pilih data yang ingin dihapus terlebih dahulu.");
            }
        }

        private void SetActionData(Transaksi transaksi)
        {
            tb_kode.Text = transaksi.kode;
            tb_total.Text = transaksi.total.ToString();
            tb_currency.Text = transaksi.currency;
            tb_buy.Text = transaksi.buy.ToString();
            tb_sell.Text = transaksi.sell.ToString();
            dateTime_action.Value = transaksi.date;

            lbl_kode.Text = transaksi.kode;
            lbl_total.Text = transaksi.total.ToString() + " " + transaksi.currency;
            lbl_buy.Text = transaksi.buy.ToString();
            lbl_sell.Text = transaksi.sell.ToString();
            lbl_date.Text = transaksi.date.ToString("dd/MM/yyyy");
            lbl_profit.Text = transaksi.profit.ToString() + "%";
            if(transaksi.profit < 0) lbl_profit.ForeColor = Color.Red;
            else if(transaksi.profit > 0) lbl_profit.ForeColor = Color.Green;
        }

        private void FetchListData()
        {
            listview_transaksi.Items.Clear();

            foreach(Transaksi transaksi in DB_Transaksi.Read_All_Transaksi())
            {
                SetListItemData(transaksi);
            }
        }

        private void FetchQueryListData(string keyword, DateTime startDateTime, DateTime endDateTime)
        {
            listview_transaksi.Items.Clear();

            foreach (Transaksi transaksi in DB_Transaksi.Query(keyword, startDateTime, endDateTime))
            {
                SetListItemData(transaksi);
            }
        }

        private void SetListItemData(Transaksi transaksi)
        {
            ListViewItem listItem = new ListViewItem(transaksi.id.ToString());
            listItem.UseItemStyleForSubItems = false;
            listItem.SubItems.Add(transaksi.kode);
            listItem.SubItems.Add(transaksi.total.ToString());
            listItem.SubItems.Add(transaksi.currency.ToString());
            listItem.SubItems.Add(transaksi.buy.ToString());
            listItem.SubItems.Add(transaksi.sell.ToString());

            Color profitColor = Color.Black;
            if (transaksi.profit < 0) profitColor = Color.Red;
            else if (transaksi.profit > 0) profitColor = Color.Green;

            var profitItem = listItem.SubItems.Add(transaksi.profit.ToString("0.00") + "%");
            profitItem.ForeColor = profitColor;

            listItem.SubItems.Add(transaksi.date.ToString("dd/MM/yyyy"));

            listview_transaksi.Items.Add(listItem);
        }

        private void btn_aksi_Click(object sender, EventArgs e)
        {
            if (btn_aksi.Text == "Hapus")
            {
                DB_Transaksi.Delete_Transaksi(int.Parse(listview_transaksi.SelectedItems[0].Text));

                FetchMainData();
                FetchListData();
                tabControl1.SelectTab(tabPage2);
                HideTab();
                return;
            }

            Transaksi transaksi = new Transaksi();

            bool error = (tb_kode.TextLength < 2) || (tb_total.Value == 0) || (tb_currency.TextLength < 2) || (tb_buy.Value == 0) || (tb_sell.Value == 0);

            if ((!error) && (errorProvider1.GetError(tb_kode) == "") && (errorProvider1.GetError(tb_currency) == "") && (errorProvider1.GetError(tb_buy) == "") && (errorProvider1.GetError(tb_sell) == ""))
            {
                transaksi.id = 0;
                transaksi.kode = tb_kode.Text;
                transaksi.total = (double)tb_total.Value;
                transaksi.currency = tb_currency.Text;
                transaksi.buy = (double)tb_buy.Value;
                transaksi.sell = (double)tb_sell.Value;
                transaksi.date = dateTime_action.Value;
                transaksi.profit = ((double)tb_sell.Value - (double)tb_buy.Value) / (double)tb_buy.Value * 100;
            }
            else
            {
                MessageBox.Show("Mohon untuk memperbaiki data yang tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(btn_aksi.Text == "Ubah")
            {
                DB_Transaksi.Update_Transaksi(transaksi, int.Parse(listview_transaksi.SelectedItems[0].Text));
            }
            else if(btn_aksi.Text == "Tambah")
            {
                DB_Transaksi.Add_Transaksi(transaksi);
            }

            FetchMainData();
            FetchListData();
            tabControl1.SelectTab(tabPage2);
            HideTab();
        }
        private void ShowTab()
        {
            if (tabControl1.TabCount < 3)
            {
                tabControl1.TabPages.Add(tabPage3);
            }

            tabControl1.SelectTab(tabPage3);
        }

        private void HideTab()
        {
            ResetActionData();
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void tb_kode_Validating(object sender, CancelEventArgs e)
        {
            if (tb_kode.TextLength < 2)
            {
                errorProvider1.SetError(tb_kode, "Kode harus lebih dari 1 huruf");
                lbl_kode.Text = "-";
            }
            else
            {
                errorProvider1.SetError(tb_kode, "");
                lbl_kode.Text = tb_kode.Text;
            }
        }

        private void tb_currency_Validating(object sender, CancelEventArgs e)
        {
            if (tb_currency.TextLength < 2)
            {
                errorProvider1.SetError(tb_currency, "Currency harus lebih dari 1 huruf");
                lbl_total.Text = tb_total.Value + " -";
            }
            else
            {
                errorProvider1.SetError(tb_currency, "");
                lbl_total.Text = tb_total.Value + " " + tb_currency.Text;
            }
        }

        private void tb_buy_Validating(object sender, EventArgs e)
        {
            if(tb_buy.Value >= tb_buy.Increment)
            {
                errorProvider1.SetError(tb_buy, "");
                lbl_buy.Text = tb_buy.Text;

                double profit = ((double)tb_sell.Value - (double)tb_buy.Value) / (double)tb_buy.Value * 100;
                lbl_profit.Text = profit.ToString() + "%";

                if (profit < 0) lbl_profit.ForeColor = Color.Red;
                else if (profit > 0) lbl_profit.ForeColor = Color.Green;
                else lbl_profit.ForeColor = Color.Black;
            }
            else
            {
                errorProvider1.SetError(tb_buy, "Buy harus lebih 0");
                lbl_buy.Text = "-";
                lbl_profit.Text = "-";
            }
        }

        private void tb_sell_Validating(object sender, EventArgs e)
        {
            if(tb_buy.Value >= tb_buy.Increment)
            {
                errorProvider1.SetError(tb_sell, "");
                lbl_sell.Text = tb_sell.Text;

                double profit = ((double)tb_sell.Value - (double)tb_buy.Value) / (double)tb_buy.Value * 100;
                lbl_profit.Text = profit.ToString() + "%";

                if (profit < 0) lbl_profit.ForeColor = Color.Red;
                else if (profit > 0) lbl_profit.ForeColor = Color.Green;
                else lbl_profit.ForeColor = Color.Black;
            }
            else
            {
                errorProvider1.SetError(tb_sell, "Buy harus lebih 0");
                lbl_sell.Text = "-";
                lbl_profit.Text = "-";
            }
        }

        private void dateTime_action_Validating(object sender, EventArgs e)
        {
            lbl_date.Text = dateTime_action.Text;
        }

        private void tb_total_Validating(object sender, CancelEventArgs e)
        {
            lbl_total.Text = tb_total.Value + " " + tb_currency.Text;
        }

        private void listview_transaksi_Click(object sender, EventArgs e)
        {
            if(listview_transaksi.SelectedItems[0] != null)
            {
                btn_ubah.Enabled = true;
                btn_hapus.Enabled = true;
            }
            else
            {
                btn_ubah.Enabled = false;
                btn_hapus.Enabled = false;
            }
        }

        private void btn_resetAction_Click(object sender, EventArgs e)
        {
            ResetActionData();
        }

        private void ResetActionData()
        {
            tb_kode.Text = "";
            tb_total.Value = 0;
            tb_currency.Text = "";
            tb_buy.Value = 0;
            tb_sell.Value = 0;
            dateTime_action.Value = DateTime.Today;
            dateTime_action.Format = DateTimePickerFormat.Custom;
            dateTime_action.CustomFormat = "dd/MM/yyyy";

            lbl_kode.Text = "-";
            lbl_total.Text = "- IDR";
            lbl_buy.Text = "-";
            lbl_sell.Text = "-";
            lbl_date.Text = "-";
            lbl_profit.Text = "-";
            lbl_profit.ForeColor = Color.Black;
        }

        private void dateTime_filter_ValueChanged(object sender, EventArgs e)
        {
            if (dateTime_filter.Value > dateTime_filter_2.Value)
            {
                MessageBox.Show("Tanggal pertama tidak boleh lebih besar dari tanggal kedua");
                dateTime_filter.Value = dateTime_filter_2.Value;
            }
        }

        private void dateTime_filter_2_ValueChanged(object sender, EventArgs e)
        {
            if(dateTime_filter_2.Value < dateTime_filter.Value)
            {
                MessageBox.Show("Tanggal kedua tidak boleh lebih kecil dari tanggal pertama");
                dateTime_filter_2.Value = dateTime_filter.Value;
            }
        }

        private void dateTime_menu_start_ValueChanged(object sender, EventArgs e)
        {
            if (dateTime_menu_start.Value > dateTime_menu_end.Value)
            {
                MessageBox.Show("Tanggal pertama tidak boleh lebih besar dari tanggal kedua");
                dateTime_menu_start.Value = dateTime_menu_end.Value;
            }
            else FetchMainCustomData();
        }

        private void dateTime_menu_end_ValueChanged(object sender, EventArgs e)
        {
            if (dateTime_menu_end.Value < dateTime_menu_start.Value)
            {
                MessageBox.Show("Tanggal kedua tidak boleh lebih kecil dari tanggal pertama");
                dateTime_menu_end.Value = dateTime_menu_start.Value;
            }
            else FetchMainCustomData();
        }

        private void btn_menu_refresh_Click(object sender, EventArgs e)
        {
            FetchMainData();
            FetchMainCustomData();
        }
    }
}
