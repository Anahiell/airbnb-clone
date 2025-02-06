import { Link, useLocation } from "react-router-dom";
import styles from "../styles/Header.module.css";

const Header = ({ onOpenModal, user, onLogout, onToggleMap }) => {
  const location = useLocation(); // Получаем текущий URL

  return (
    <header className={styles.header}>
      {/* Логотип */}
      <Link to="/" className={styles.logo}>HomeFU</Link>

      {/* Поисковая строка */}
      <div className={styles.searchBar}>
        <input type="text" placeholder="Поиск помещения" />
        <button>🔍</button>
      </div>

      {/* Кнопки справа */}
      <div className={styles.rightButtons}>
        <Link to="/listings" className={styles.offerBtn}>Запропонувати помещение</Link>

        {/* Кнопка "Показать на карте" видна только на странице listing */}
        {location.pathname === "/listings" && (
          <button className={styles.mapButton} onClick={onToggleMap}>📍 Показать на карте</button>
        )}

        {user ? (
          <div className={styles.userMenu}>
            <span>Привет, {user.username}</span>
            <button className={styles.logoutBtn} onClick={onLogout}>Выйти</button>
          </div>
        ) : (
          <button className={styles.profileIcon} onClick={onOpenModal}>👤</button>
        )}
      </div>
    </header>
  );
};

export default Header;
