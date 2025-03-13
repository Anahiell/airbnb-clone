import styles from "./CategoryFilters.module.css";
import PropTypes from "prop-types";
const categories = [
  { id: "all", label: "Все" },
  { id: "house", label: "Дома" },
  { id: "apartment", label: "Апартаменты" },
  { id: "villa", label: "Виллы" },
  { id: "cabin", label: "Хижины" },
];

const CategoryFilters = ({ selectedCategory, onSelectCategory }) => {
  return (
    <div className={styles.filters}>
      {categories.map(({ id, label }) => (
        <button
          key={id}
          className={`${styles.filter} ${selectedCategory === id ? styles.active : ""}`}
          onClick={() => onSelectCategory(id)}
        >
          {label}
        </button>
      ))}
    </div>
  );
};

//Добавляем валидацию пропсов
CategoryFilters.propTypes = {
  selectedCategory: PropTypes.string.isRequired,  // `selectedCategory` должен быть строкой
  onSelectCategory: PropTypes.func.isRequired,   // `onSelectCategory` должен быть функцией
};

export default CategoryFilters;
