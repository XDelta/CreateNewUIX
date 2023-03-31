using BaseX;
using FrooxEngine;
using FrooxEngine.UIX;

using NeosModLoader;

namespace CreateNewUIX {
	public class CreateNewUIX : NeosMod {
		public override string Name => "CreateNewUIX";
		public override string Author => "Delta";
		public override string Version => "1.1.0";
		public override string Link => "https://github.com/XDelta/CreateNewUIX";
		public override void OnEngineInit() {
			/*Harmony harmony = new Harmony("net.deltawolf.CreateNewUIX");
			harmony.PatchAll();*/
			Engine.Current.RunPostInit(AddMenuOptions);
		}
		private static readonly float3 defaultScale = new float3(0.001f, 0.001f, 0.001f);
		void AddMenuOptions() {
			DevCreateNewForm.AddAction("Object/Neos UIX", "Button", (x) => CreateButton(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Checkbox", (x) => CreateCheckBox(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Expander", (x) => CreateExpander(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Image", (x) => CreateImage(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Numeric UpDown", (x) => CreateNumericUpDownButtons(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Panel", (x) => CreatePanel(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Progress Bar", (x) => CreateProgressBar(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Radio", (x) => CreateRadio(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Scroll Area", (x) => CreateScrollArea(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Slider", (x) => CreateSlider(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Text", (x) => CreateText(x));
			DevCreateNewForm.AddAction("Object/Neos UIX", "Text Field", (x) => CreateTextField(x));
		}

		/*
			*Button
			*Checkbox
			Dropdown
			+Expander/collaspable
			Horizontal Choice Bar
			Vertical Button layout
			+Image
			Numberic Choice Bar
			/Numeric UpDown
			*Panel
			*Progress Bar
			*Radio
			*Slider
			*Text Field
			URL Field with button to open as link
		*/

		public static void CreateButton(Slot x) {
			x.Name = "Button";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(256, 48);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.Black, true);
			ui.Button("Button");
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}

		public static void CreateCheckBox(Slot x) {
			x.Name = "Checkbox";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(48, 48);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Checkbox();
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateExpander(Slot x) {
			x.Name = "Expander";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(256, 256);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.SplitVertically(0.2f, out RectTransform r1, out RectTransform r2, 0f);
			ui.NestInto(r1);
			ui.Button("Expander");
			var ex = ui.Current.AttachComponent<Expander>();
			ui.NestInto(r2);
			ui.Panel(color.White, true);
			var text = ui.Text("Some Text");
			text.Color.Value = color.Black;
			ex.SectionRoot.Target = r2.Slot;
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateImage(Slot x) {
			x.Name = "Image";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(256, 256);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Image(color.White, true).Sprite.Target = ui.Root.AttachSprite(NeosAssets.Common.Icons.Camera, uncompressed: false, evenNull: false, getExisting: true, null);
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateNumericUpDownButtons(Slot x) {
			x.Name = "Numeric UpDown";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			var _dataValue = x.AttachComponent<ValueField<float>>();
			_canvas.Size.Value = new float2(256, 48);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.Black, true);
			ui.HorizontalLayout(5, 2);
			var dec = ui.Button("-");
			var text = ui.Text("0.00");
			var inc = ui.Button("+");
			text.Color.Value = color.White;
			text.Slot.GetComponent<LayoutElement>().PreferredWidth.Value = 108;
			//Decrement
			var bvs = dec.Slot.AttachComponent<ButtonValueShift<float>>();
			bvs.TargetValue.Target = _dataValue.Value;
			bvs.Delta.Value = -1f;
			//Increment
			var bvs2 = inc.Slot.AttachComponent<ButtonValueShift<float>>();
			bvs2.TargetValue.Target = _dataValue.Value;
			bvs2.Delta.Value = 1f;
			//Text Driver from value
			var vtfd = text.Slot.AttachComponent<ValueTextFormatDriver<float>>();
			vtfd.Text.Target = text.Content;
			vtfd.Format.Value = "{0:f2}";
			vtfd.Source.Target = _dataValue.Value;
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreatePanel(Slot x) {
			x.Name = "Panel";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(1920, 1080);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.Black, true);
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateProgressBar(Slot x) {
			x.Name = "ProgressBar";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(512, 64);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.Black, true);
			Image image = ui.Image(color.White, false);
			ProgressBar progressBar = image.Slot.AttachComponent<ProgressBar>(true, null);
			progressBar.SetTarget(image.RectTransform);
			progressBar.Progress.Value = 0.69f; //Start with a value to show the progress bar 
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateRadio(Slot x) {
			x.Name = "Radio";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			var _dataValue = x.AttachComponent<ValueField<int>>();
			_canvas.Size.Value = new float2(256, 208);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.Black, true);
			RadiantUI_Constants.SetupDefaultStyle(ui, false);
			ui.VerticalLayout(4f, 0f, null);
			ui.FitContent(SizeFit.Disabled, SizeFit.PreferredSize);
			ui.Style.MinHeight = 48f;
			ui.Style.PreferredHeight = 48f;
			for (int i = 0; i < 4; i++) {
				ui.ValueRadio<int>("Button "+ i,_dataValue.Value, i);
			}
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateScrollArea(Slot x) {
			x.Name = "Scroll Area";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(512, 512);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.White, true);
			ui.ScrollArea(null);
			ui.VerticalLayout(0f, 10f, null);
			ui.FitContent(SizeFit.Disabled, SizeFit.MinSize);
			ui.Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec fermentum magna sapien. Vestibulum tempus tortor a ante pretium ultricies. In ligula lorem, varius quis malesuada ut, tincidunt at mi. Aenean non ante id ex faucibus convallis ac vitae mauris. Integer ante ex, porta ut sapien et, suscipit semper lorem. Vivamus elementum dui vitae risus gravida fermentum. Quisque aliquet rhoncus nunc, ac sagittis turpis mattis a. Cras rhoncus nunc a tortor egestas, eget posuere elit finibus. In hac habitasse platea dictumst. Maecenas lobortis elit sed neque porta, a mattis mauris mollis. Nunc condimentum augue ullamcorper pretium molestie. Praesent lacus tellus, sagittis eu imperdiet sagittis, tincidunt nec quam. Donec in lorem ac nisl suscipit consectetur at at enim.\r\n\r\nMaecenas vitae magna id nunc ultrices interdum finibus commodo nunc. In lorem mi, vehicula molestie tortor luctus, molestie congue nunc. Praesent aliquet mollis rhoncus. Cras rutrum nec augue ac lobortis. Sed viverra justo a laoreet imperdiet. Nam egestas fermentum magna, et varius lectus tempus ac. Vestibulum sed libero nunc. Donec tempus euismod urna vel dapibus. Vivamus nec enim nisi. In feugiat, lectus sed mattis vulputate, augue nisi efficitur turpis, ac vestibulum justo risus nec arcu. Nulla et lorem id nibh consectetur congue. Fusce vel turpis eget magna consectetur semper. Duis suscipit, dui eu suscipit accumsan, nisl mi ullamcorper elit, vitae congue est nunc a libero. Integer eget mi dui. Phasellus vel elit scelerisque, consectetur quam cursus, tincidunt tortor.\r\n\r\nOrci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Phasellus tempor gravida purus quis volutpat. Suspendisse tortor felis, consequat nec porta in, imperdiet id arcu. Curabitur ut sagittis ligula, eget lacinia nisl. Vestibulum vulputate dui nec metus tincidunt, sed efficitur sapien tempor. Nunc libero nulla, dictum tempor euismod quis, posuere non metus. Aenean enim ligula, luctus sed turpis eget, dapibus tincidunt dolor.\r\n\r\nDuis lorem mi, aliquam at maximus vel, scelerisque sit amet ex. Sed quis congue nulla. Sed eu maximus lacus, sed convallis ante. Aliquam ligula lectus, vulputate id est non, semper accumsan arcu. Nam elementum massa velit, sodales dapibus felis porta vel. Fusce a nibh id nibh molestie venenatis. Cras ac tincidunt nisi. Cras gravida molestie risus, in pharetra turpis blandit vitae. Phasellus enim felis, vestibulum in lacus ut, vestibulum lacinia velit.", 16, false, Alignment.TopLeft).Color.Value = color.Black;
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateSlider(Slot x) {
			x.Name = "Slider";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(512, 64);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.Black, true);
			ui.Slider(64f, 0.5f,0f, 1f, false);
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateText(Slot x) {
			x.Name = "Text";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(256, 48);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.White, true);
			var text = ui.Text("Text");
			text.Color.Value = color.Black;
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
		public static void CreateTextField(Slot x) {
			x.Name = "Text Field";
			x.GlobalScale = defaultScale;
			x.AttachComponent<Grabbable>().Scalable.Value = true;
			var _canvas = x.AttachComponent<Canvas>();
			_canvas.Size.Value = new float2(256, 48);
			UIBuilder ui = new UIBuilder(_canvas);
			ui.Panel(color.Black, true);
			TextField text = ui.TextField(null);
			text.Text.NullContent.Value = "<i>Text Field</i>";
			text.Editor.Target.FinishHandling.Value = TextEditor.FinishAction.NullOnWhitespace;
			ui.CurrentRect.AddFixedPadding(6);
			x.PositionInFrontOfUser(float3.Backward, distance: 1f);
		}
	}
}