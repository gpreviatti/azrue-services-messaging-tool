namespace Host.WinFormsApp;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        comboBoxProfile=new ComboBox();
        buttonCreateProfile=new Button();
        buttonDeleteProfile=new Button();
        labelSelectProfile=new Label();
        textBoxData=new TextBox();
        labelData=new Label();
        labelResult=new Label();
        textBoxResult=new TextBox();
        buttonListen=new Button();
        buttonLoadData=new Button();
        buttonSendData=new Button();
        numericUpDownRepeat=new NumericUpDown();
        label4=new Label();
        openFileDialog1=new OpenFileDialog();
        ((System.ComponentModel.ISupportInitialize)numericUpDownRepeat).BeginInit();
        SuspendLayout();
        // 
        // comboBoxProfile
        // 
        comboBoxProfile.FormattingEnabled=true;
        comboBoxProfile.Location=new Point(11, 50);
        comboBoxProfile.Name="comboBoxProfile";
        comboBoxProfile.Size=new Size(297, 23);
        comboBoxProfile.TabIndex=0;
        // 
        // buttonCreateProfile
        // 
        buttonCreateProfile.Location=new Point(324, 50);
        buttonCreateProfile.Name="buttonCreateProfile";
        buttonCreateProfile.Size=new Size(124, 23);
        buttonCreateProfile.TabIndex=1;
        buttonCreateProfile.Text="Create profile";
        buttonCreateProfile.UseVisualStyleBackColor=true;
        // 
        // buttonDeleteProfile
        // 
        buttonDeleteProfile.Location=new Point(454, 50);
        buttonDeleteProfile.Name="buttonDeleteProfile";
        buttonDeleteProfile.Size=new Size(124, 23);
        buttonDeleteProfile.TabIndex=2;
        buttonDeleteProfile.Text="Delete profile";
        buttonDeleteProfile.UseVisualStyleBackColor=true;
        // 
        // labelSelectProfile
        // 
        labelSelectProfile.AutoSize=true;
        labelSelectProfile.Location=new Point(11, 18);
        labelSelectProfile.Name="labelSelectProfile";
        labelSelectProfile.Size=new Size(75, 15);
        labelSelectProfile.TabIndex=3;
        labelSelectProfile.Text="Select profile";
        // 
        // textBoxData
        // 
        textBoxData.Location=new Point(11, 106);
        textBoxData.Multiline=true;
        textBoxData.Name="textBoxData";
        textBoxData.Size=new Size(567, 184);
        textBoxData.TabIndex=3;
        // 
        // labelData
        // 
        labelData.AutoSize=true;
        labelData.Location=new Point(11, 88);
        labelData.Name="labelData";
        labelData.Size=new Size(31, 15);
        labelData.TabIndex=5;
        labelData.Text="Data";
        // 
        // labelResult
        // 
        labelResult.AutoSize=true;
        labelResult.Location=new Point(12, 349);
        labelResult.Name="labelResult";
        labelResult.Size=new Size(39, 15);
        labelResult.TabIndex=7;
        labelResult.Text="Result";
        // 
        // textBoxResult
        // 
        textBoxResult.BackColor=SystemColors.ControlLightLight;
        textBoxResult.Location=new Point(11, 367);
        textBoxResult.Multiline=true;
        textBoxResult.Name="textBoxResult";
        textBoxResult.ReadOnly=true;
        textBoxResult.Size=new Size(567, 206);
        textBoxResult.TabIndex=6;
        // 
        // buttonListen
        // 
        buttonListen.Location=new Point(454, 309);
        buttonListen.Name="buttonListen";
        buttonListen.Size=new Size(124, 23);
        buttonListen.TabIndex=7;
        buttonListen.Text="Listen";
        buttonListen.UseVisualStyleBackColor=true;
        // 
        // buttonLoadData
        // 
        buttonLoadData.Location=new Point(12, 309);
        buttonLoadData.Name="buttonLoadData";
        buttonLoadData.Size=new Size(124, 23);
        buttonLoadData.TabIndex=4;
        buttonLoadData.Text="Load data";
        buttonLoadData.UseVisualStyleBackColor=true;
        // 
        // buttonSendData
        // 
        buttonSendData.Location=new Point(324, 309);
        buttonSendData.Name="buttonSendData";
        buttonSendData.Size=new Size(124, 23);
        buttonSendData.TabIndex=6;
        buttonSendData.Text="Send data";
        buttonSendData.UseVisualStyleBackColor=true;
        // 
        // numericUpDownRepeat
        // 
        numericUpDownRepeat.Location=new Point(215, 309);
        numericUpDownRepeat.Name="numericUpDownRepeat";
        numericUpDownRepeat.Size=new Size(93, 23);
        numericUpDownRepeat.TabIndex=5;
        // 
        // label4
        // 
        label4.AutoSize=true;
        label4.Location=new Point(154, 313);
        label4.Name="label4";
        label4.Size=new Size(43, 15);
        label4.TabIndex=12;
        label4.Text="Repeat";
        // 
        // openFileDialog1
        // 
        openFileDialog1.FileName="openFileDialog1";
        // 
        // Form1
        // 
        AutoScaleDimensions=new SizeF(7F, 15F);
        AutoScaleMode=AutoScaleMode.Font;
        ClientSize=new Size(599, 591);
        Controls.Add(label4);
        Controls.Add(numericUpDownRepeat);
        Controls.Add(buttonSendData);
        Controls.Add(buttonListen);
        Controls.Add(buttonLoadData);
        Controls.Add(labelResult);
        Controls.Add(textBoxResult);
        Controls.Add(labelData);
        Controls.Add(textBoxData);
        Controls.Add(labelSelectProfile);
        Controls.Add(buttonDeleteProfile);
        Controls.Add(buttonCreateProfile);
        Controls.Add(comboBoxProfile);
        Name="Form1";
        ShowIcon=false;
        Text="Azure messaging tool";
        ((System.ComponentModel.ISupportInitialize)numericUpDownRepeat).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ComboBox comboBoxProfile;
    private Button buttonCreateProfile;
    private Button buttonDeleteProfile;
    private Label labelSelectProfile;
    private TextBox textBoxData;
    private Label labelData;
    private Label labelResult;
    private TextBox textBoxResult;
    private Button buttonListen;
    private Button buttonLoadData;
    private Button buttonSendData;
    private NumericUpDown numericUpDownRepeat;
    private Label label4;
    private OpenFileDialog openFileDialog1;
}
