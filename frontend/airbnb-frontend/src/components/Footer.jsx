import { Link } from "react-router-dom";
import styles from "../styles/Footer.module.css";

const Footer = () => {
  return (
    <footer className={styles.footer}>
      {/* Верхний блок: разделы */}
      <div className={styles.footerTop}>
        <div className={styles.column}>
          <h4>Поддержка</h4>
          <Link to="/help">Центр помощи</Link>
          <Link to="/safety">Безопасность</Link>
          <Link to="/support">Свяжитесь с нами</Link>
        </div>

        <div className={styles.column}>
          <h4>Правила</h4>
          <Link to="/terms">Условия использования</Link>
          <Link to="/privacy">Политика конфиденциальности</Link>
          <Link to="/refunds">Возврат средств</Link>
        </div>

        <div className={styles.column}>
          <h4>HomeFU</h4>
          <Link to="/about">О нас</Link>
          <Link to="/careers">Карьера</Link>
          <Link to="/blog">Блог</Link>
        </div>
      </div>

      {/* Средний блок: Соц. сети */}
      <div className={styles.footerMiddle}>
        <Link to="https://facebook.com" className={styles.social}>📘</Link>
        <Link to="https://instagram.com" className={styles.social}>📸</Link>
        <Link to="https://twitter.com" className={styles.social}>🐦</Link>
      </div>

      {/* Нижний блок: Копирайт и язык */}
      <div className={styles.footerBottom}>
        <p>© 2024 HomeFU, Inc.</p>
        <p>🌍 Язык: <span>Українська (UA) | USD</span></p>
      </div>
    </footer>
  );
};

export default Footer;
