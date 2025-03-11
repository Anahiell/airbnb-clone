import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getUser, logout } from "../utils/auth";
import styles from "../styles/ProfilePage.module.css";

const ProfilePage = () => {
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const currentUser = getUser();
    if (!currentUser) {
      navigate("/sign-in");
    }
    setUser(currentUser);
  }, [navigate]);

  return (
    <div className={styles.profile}>
      {user ? (
        <>
          <h2>Привет, {user.email}!</h2>
          <button onClick={() => { logout(); navigate("/"); }}>Выйти</button>
        </>
      ) : (
        <p>Загрузка...</p>
      )}
    </div>
  );
};

export default ProfilePage;
