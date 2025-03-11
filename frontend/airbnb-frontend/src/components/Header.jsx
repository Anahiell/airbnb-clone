import { useState, useEffect } from "react";
import { Link, useLocation } from "react-router-dom";
import SearchBar from "../components/SearchBar";
import styles from "../styles/Header.module.css";

const Header = ({ onOpenModal, user, onLogout, onToggleMap }) => {
  const location = useLocation();
  const [isActive, setIsActive] = useState(false);
  let timer;

  const handleFocus = () => {
    clearTimeout(timer);
    setIsActive(true);
  };

  const handleBlur = () => {
    timer = setTimeout(() => setIsActive(false), 5000);
  };

  useEffect(() => {
    return () => clearTimeout(timer);
  }, []);

  return (
    <header className={`${styles.header} ${isActive ? styles.active : ""}`}>
      <div className={styles.row}>
        <Link to="/" className={styles.logo}>HomeFU</Link>

        <nav className={styles.nav}>
          <a href="#">Варіанти помешкань</a>
          <a href="#">Враження</a>
          <a href="#">Онлайн-враження</a>
        </nav>

        <div className={styles.rightButtons}>
          <Link to="/listings" className={styles.offerBtn}>Запропонувати помешкання</Link>
          {location.pathname === "/listings" && (
            <button className={styles.mapButton} onClick={onToggleMap}>📍 Показати на карті</button>
          )}
          {user ? (
            <div className={styles.userMenu}>
              <span>Привет, {user.username}</span>
              <button className={styles.logoutBtn} onClick={onLogout}>Выйти</button>
            </div>
          ) : (
            <button className={styles.ovalButton} onClick={onOpenModal}>👤</button>
          )}
        </div>
      </div>

      <div className={styles.searchContainer} onMouseEnter={handleFocus} onMouseLeave={handleBlur}>
        <SearchBar onFocus={handleFocus} onBlur={handleBlur} />
      </div>
    </header>
  );
};

export default Header;
