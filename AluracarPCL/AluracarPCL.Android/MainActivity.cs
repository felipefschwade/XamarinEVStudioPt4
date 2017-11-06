using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AluracarPCL.Media;
using AluracarPCL.Droid;
using Xamarin.Forms;
using Android.Content;
using Android.Provider;

[assembly: Xamarin.Forms.Dependency(typeof(MainActivity))]
namespace AluracarPCL.Droid
{
    [Activity(Label = "AluracarPCL", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ICamera
    {
        static Java.IO.File imagem;
        public void TirarFoto()
        {
             imagem = GetArquivoImagen();

            var intent = new Intent(MediaStore.ActionImageCapture);
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imagem));
            var activity = Forms.Context as Activity;
            activity.StartActivityForResult(intent, 0);
        }

        private static Java.IO.File GetArquivoImagen()
        {
            Java.IO.File dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                        Android.OS.Environment.DirectoryPictures), "Imagens");
            if (!dir.Exists()) dir.Mkdirs();
            Java.IO.File imagem = new Java.IO.File(dir, "FotoPerfil.jpg");
            return imagem;
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            byte[] bytes;
            using (var stream = new Java.IO.FileInputStream(imagem))
            {
                bytes = new byte[imagem.Length()];
                stream.Read(bytes);
            }
            if (resultCode == Result.Ok)
            {
                MessagingCenter.Send<byte[]>(bytes, "Foto");
            }
        }

    }
}

