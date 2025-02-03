import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getUser, logout } from "../utils/auth";
import styles from "../styles/ProfilePage.module.css";

const ProfilePage = () => {
  const [user, setUser] = useState(null);
  const [name, setName] = useState("");
  const [bio, setBio] = useState("");
  const [avatar, setAvatar] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const currentUser = getUser();
    if (!currentUser) {
      navigate("/");
    } else {
      setUser(currentUser);
      setName(currentUser.name || "");
      setBio(currentUser.bio || "");
      setAvatar(currentUser.avatar || "/default-avatar.png"); // Загружаем аватар
    }
  }, [navigate]);

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  const handleSave = () => {
    const updatedUser = { ...user, name, bio, avatar };
    localStorage.setItem("user", JSON.stringify(updatedUser));
    setUser(updatedUser);
    alert("✅ Профиль обновлён!");
  };

  const handleAvatarChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        setAvatar(reader.result);
      };
      reader.readAsDataURL(file);
    }
  };

  if (!user) return null;

  return (
    <div className={styles.profilePage}>
      <div className={styles.profileCard}>
        <label className={styles.avatarUpload}>
          <input type="file" accept="image/*" onChange={handleAvatarChange} />
          <img src={avatar} alt="Avatar" className={styles.avatar} />
        </label>
        <h1>{name || "Без имени"}</h1>
        <p className={styles.bio}>{bio || "О себе..."}</p>
        <input
          type="text"
          value={name}
          placeholder="Имя"
          onChange={(e) => setName(e.target.value)}
        />
        <textarea
          value={bio}
          placeholder="О себе"
          onChange={(e) => setBio(e.target.value)}
        />
        <button className={styles.saveBtn} onClick={handleSave}>💾 Сохранить</button>
        <button className={styles.logoutBtn} onClick={handleLogout}>🚪 Выйти</button>
      </div>
    </div>
  );
};

export default ProfilePage;
