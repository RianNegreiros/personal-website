import styles from './header.module.css'

export function Header() {
  return (
    <header>
    <a href="" className={styles.headerLinkLogo}>My Blog</a>
    <nav className={styles.headerNav}>
      <a href="">Login</a>
      <a href="">Register</a>
    </nav>
  </header>
  )
}