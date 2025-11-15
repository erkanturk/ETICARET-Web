using System.Net;
using System.Net.Mail;

namespace ETICARET.WebUI.EmailService
{
    // MailHelper: uygulama içinde e-posta gönderme işlemini kolaylaştıran yardımcı sınıf.
    // static olarak tanımlanmıştır, yani bir nesne oluşturmadan doğrudan kullanılabilir.
    public static class MailHelper
    {
        // Bu public metod, tek bir alıcıya mail göndermek için kullanılır.
        // "body" -> mail içeriği, 
        // "to" -> alıcının e-posta adresi,
        // "subject" -> mail konusu,
        // "isHtml" -> içeriğin HTML formatında olup olmadığını belirler.
        public static bool SendMail(string body, string to, string subject, bool isHtml = true)
        {
            // Tek bir kişiye mail gönderilecekse, o kişiyi bir listeye çevirip alttaki private metoda yollar.
            return SendMail(body, new List<string>() { to }, subject, isHtml);
        }

        // Bu private metod, birden fazla alıcıya mail göndermek için kullanılır.
        // Mail gönderme işleminin asıl mantığı burada yazılıdır.
        private static bool SendMail(string body, List<string> to, string subject, bool isHtml)
        {
            bool result = false;

            try
            {
                // Gönderilecek e-posta mesajını oluştururuz.
                var message = new MailMessage();

                // Gönderen adres (mail hizmetinin bağlı olduğu hesap)
                message.From = new MailAddress("ubymailsistem@gmail.com");

                // Alıcı listesinde yer alan tüm adresler eklenir.
                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });

                // Konu, içerik ve HTML ayarları yapılır.
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                // SMTP istemcisi oluşturulur (Simple Mail Transfer Protocol)
                // Gmail’in SMTP sunucusu: smtp.gmail.com, port: 587 (TLS bağlantısı)
                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    // SSL (güvenli bağlantı) aktif edilir.
                    smtp.EnableSsl = true;

                    // SMTP kimlik bilgileri tanımlanır.
                    smtp.Credentials = new NetworkCredential(
                        "ubymailsistem@gmail.com", // Gönderen adres
                        "iicd aqam xhti fyra"                       // Gmail uygulama şifresi
                    );

                    // Varsayılan Windows kimlik bilgilerini kullanmaz.
                    smtp.UseDefaultCredentials = false;

                    // Mail gönderme işlemi yapılır.
                    smtp.Send(message);
                    result = true;
                }
            }
            catch (Exception e)
            {
                // Herhangi bir hata durumunda konsola hata bilgisi yazılır.
                Console.WriteLine(e);
                result = false;
            }

            // Mail gönderme işleminin başarılı olup olmadığını döndürür.
            return result;
        }
    }
}
