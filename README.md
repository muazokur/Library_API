1-Testleri gerçekleştirebilmek için öncelikle AuthController üzerinden admin hesabıyla giriş yapmalısınız.
{
  "email": "mmo@hotmail.com",
  "password": "LL123456."
}

2-Giriş yaptıktan sonra UserController üzerinden yeni bir kullanıcı eklemelisiniz.

3-Ardından, AuthorController aracılığıyla mevcut kullanıcılardan istediğinizi yazar olarak yetkilendirebilirsiniz.

4-Kitap ekleme, güncelleme ve silme işlemleri için yazar olarak yetkilendirdiğiniz bir kullanıcıyla giriş yapmalısınız. Ancak, kitap listeleme işlemini tüm kullanıcılar gerçekleştirebilir.

5-Her yazar yalnızca kendi eklediği kitaplar üzerinde işlem yapabilir.