import { Link, useLocation } from "react-router-dom";
import styles from "../styles/Header.module.css";

const Header = ({ onOpenModal, user, onLogout, onToggleMap }) => {
  const location = useLocation(); // Получаем текущий URL

  return (
    <header className={styles.header}>
      <div className={styles.row + " " + styles["top-row"]}>
        {/* Логотип */}
        <Link to="/" className={styles.logo}>HomeFU</Link>

        <nav className={styles.nav}>
          <a href="#">Варіанти помешкань</a>
          <a href="#">Враження</a>
          <a href="#">Онлайн-враження</a>
        </nav>

        {/* Кнопки справа */}
        <div className={styles.rightButtons}>
          <Link to="/listings" className={styles.offerBtn}>Запропонувати помешкання на HomeFU</Link>
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
            // <button className={styles.profileIcon} onClick={onOpenModal}>👤</button>
            <button className={styles.ovalButton} onClick={onOpenModal}>
              <span></span>
              <div className={styles.personContainer}>
                <div class={styles.personHead}></div>
                <div class={styles.personBody}></div>
              </div>
            </button>
          )}
        </div>
      </div>
      <div className={styles.row + " " + styles["bottom-row"]}>
        {/* Поисковая строка */}
        <div className={styles.searchBar}>
          <input type="text" placeholder="Поиск помещения" />
          <button>🔍</button>
        </div>

      </div>

    </header>
  );
};

export default Header;
