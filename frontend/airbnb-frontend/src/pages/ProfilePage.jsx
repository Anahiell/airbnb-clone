import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getProfile, updateProfile, uploadAvatar } from "../services/profileService";
import styles from "../styles/ProfilePage.module.css";

const ProfilePage = () => {
  const navigate = useNavigate();
  const [profileData, setProfileData] = useState({
    fullName: "",
    education: "",
    location: "",
    birthYear: "",
    hobbies: "",
    skills: "",
    about: "",
    avatar: "",
  });

  /** Получаем профиль при загрузке страницы 
  useEffect(() => {
    getProfile()
      .then((data) => {
        console.log("Ответ API профиля:", data); // ➤ Логируем ответ
        setProfileData(data);
      })
      .catch((error) => {
        console.error("Ошибка загрузки профиля:", error);
        navigate("/"); // Если нет авторизации, кидаем на главную
      });
  }, [navigate]);
  
*/
  /** Обработчик изменения полей */
  const handleChange = (e) => {
    setProfileData({ ...profileData, [e.target.name]: e.target.value });
  };

  /** Сохранение профиля */
  const handleSave = async () => {
    try {
      await updateProfile(profileData);
      alert("✅ Профиль сохранен!");
    } catch (error) {
      alert("❌ Ошибка при сохранении профиля");
    }
  };

  /** Загрузка фото профиля */
  const handleAvatarUpload = async (e) => {
    const file = e.target.files[0];
    if (!file) return;

    try {
      const response = await uploadAvatar(file);
      setProfileData((prev) => ({ ...prev, avatar: response.avatarUrl }));
    } catch (error) {
      alert("❌ Ошибка загрузки фото");
    }
  };

  return (
    <div className={styles.profileContainer}>
      <div className={styles.profileHeader}>
        <div className={styles.profileImage}>
          <img src={profileData.avatar || "default-avatar.png"} alt="Avatar" className={styles.avatar} />
          <input type="file" onChange={handleAvatarUpload} />
        </div>
        <div className={styles.profileInfo}>
          <h1>Ваш профиль</h1>
          <p>Информация будет использоваться на <b>HomeFU</b>, чтобы гости и хозяева могли с вами познакомиться.</p>
        </div>
      </div>

      <div className={styles.profileForm}>
        <label>Образование</label>
        <input type="text" name="education" value={profileData.education} onChange={handleChange} />

        <label>Место проживания</label>
        <input type="text" name="location" value={profileData.location} onChange={handleChange} />

        <label>Год рождения</label>
        <input type="number" name="birthYear" value={profileData.birthYear} onChange={handleChange} />

        <label>Увлечения</label>
        <input type="text" name="hobbies" value={profileData.hobbies} onChange={handleChange} />

        <label>Ключевые навыки</label>
        <input type="text" name="skills" value={profileData.skills} onChange={handleChange} />

        <label>О себе</label>
        <textarea name="about" value={profileData.about} onChange={handleChange} />

        <button className={styles.saveBtn} onClick={handleSave}>Сохранить</button>
      </div>
    </div>
  );
};

export default ProfilePage;
