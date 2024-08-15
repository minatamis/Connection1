﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connection1.Model
{
    public class ButtonConfig
    {
        public Size Size { get; set; }
        public string Text { get; set; }
        public Font Font { get; set; }
        public string TagLine { get; set; } = "";
        public int Id { get; set; } = 0;
        public double price { get; set; } = double.NaN;
        public string ImagePath { get; set; }
        public ContentAlignment ImageAlign { get; set; }
        public ContentAlignment TextAlign { get; set; }
        public TextImageRelation TextImageRelation { get; set; }
        public Padding Padding { get; set; }

        public ButtonConfig()
        {
            // Default values
            Size = new Size(163, 56);
            Text = "Hot Sizzling";
            Font = new Font("Book Antiqua", 12, FontStyle.Bold);
            ImageAlign = ContentAlignment.MiddleLeft;
            TextAlign = ContentAlignment.MiddleCenter;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            Padding = new Padding(5, 5, 0, 0);
        }
    }

    public static class ButtonFactory
    {
        public static Button CreateButton(ButtonConfig config)
        {
            Button button = new Button
            {
                Size = config.Size,
                Text = config.Text,
                Font = config.Font,
                //Image = Image.FromFile(config.ImagePath),
                ImageAlign = config.ImageAlign,
                TextAlign = config.TextAlign,
                TextImageRelation = config.TextImageRelation,
                Padding = config.Padding,
                Tag = (config.Id, config.TagLine, config.price)
            };
            return button;
        }
    }

    public class ButtonSize
    {
        public int x { get; set; } = 162;
        public int y { get; set; } = 142;
        public int sizeW { get; set; } = 163;
        public int sizeH { get; set; } = 56;
        public bool start { get; set; } = true;
    }
}
