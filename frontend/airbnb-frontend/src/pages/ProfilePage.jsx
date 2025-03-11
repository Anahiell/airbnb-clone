import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import styles from "../styles/ProfilePage.module.css";

const ProfilePage = () => {
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [profileData, setProfileData] = useState({
    fullName: "",
    education: "",
    location: "",
    birthYear: "",
    hobbies: "",
    skills: "",
    about: "",
  });

  useEffect(() => {
    const storedUser = JSON.parse(localStorage.getItem("user"));
    if (!storedUser) {
      navigate("/");
    } else {
      setUser(storedUser);
      setProfileData(storedUser.profile || {});
    }
  }, [navigate]);

  const handleChange = (e) => {
    setProfileData({ ...profileData, [e.target.name]: e.target.value });
  };

  const handleSave = () => {
    const updatedUser = { ...user, profile: profileData, isNew: false };
    localStorage.setItem("user", JSON.stringify(updatedUser));
    setUser(updatedUser);
  };

  return (
    <div className={styles.profileContainer}>
      <div className={styles.profileHeader}>
        <div className={styles.profileImage}>
          <span className={styles.initial}>I</span>
          <button className={styles.uploadBtn}>📷 Добавить</button>
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
