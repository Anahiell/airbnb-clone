import { Link } from "react-router-dom";
import styles from "../styles/Header.module.css";

const Header = ({ onOpenModal }) => {
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
        <button className={styles.profileIcon} onClick={onOpenModal}>👤</button>
        <button className={styles.menuIcon}>☰</button>
      </div>
    </header>
  );
};

export default Header;
