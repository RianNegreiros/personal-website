import './globals.css'
import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import Navbar from './components/Navbar'
import { Providers } from './components/Providers'
import siteMetadata from './utils/siteMetaData'
import FloatingButtons from './components/FloatingButtons'
import Footer from './components/Footer'
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Head from 'next/head'

const inter = Inter({ subsets: ['latin'] })

export const metadata: Metadata = {
  metadataBase: new URL(siteMetadata.siteUrl),
  title: {
    template: `%s | ${siteMetadata.title}`,
    default: siteMetadata.title,
  },
  description: siteMetadata.description,
  icons: siteMetadata.siteLogo,
  openGraph: {
    title: siteMetadata.title,
    description: siteMetadata.description,
    url: siteMetadata.siteUrl,
    siteName: siteMetadata.title,
    locale: "pt_BR",
    type: "website",
    emails: siteMetadata.email
  },
  robots: {
    index: true,
    follow: true,
    googleBot: {
      index: true,
      follow: false,
      noimageindex: true,
      'max-image-preview': 'large',
      'max-video-preview': -1,
      'max-snippet': -1,
    },
  }
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en" suppressHydrationWarning>
      <Head>
        <script type="application/ld+json">
          {`
            {
              "@context": "http://schema.org",
              "@type": "Person",
              "name": "Rian Negreiros Dos Santos",
              "url": "${siteMetadata.siteUrl}",
              "sameAs": [
                "${siteMetadata.github}",
                "${siteMetadata.linkedin}"
              ]
            }
        `}
        </script>
      </Head>
      <body className={`${inter.className} bg-white text-black dark:bg-gray-900 dark:text-white h-full selection:bg-gray-50 dark:selection:bg-gray-800`}>
        <Providers>
          <ToastContainer />
          <Navbar />
          <main className='flex flex-col min-h-screen max-w-6xl mx-auto px-4 sm:px-6 lg:px-8'>
            {children}
          </main>
          <FloatingButtons />
        </Providers>
        <Footer />
      </body>
    </html>
  )
}
