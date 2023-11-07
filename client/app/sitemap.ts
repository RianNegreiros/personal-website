import siteMetadata from "./utils/siteMetaData";
import { getFeed } from "./utils/api";

interface PostData {
  slug: string;
  createdAt: string;
}

interface Route {
  url: string;
  lastModified: string;
}

function formatPostURL(slug: string): string {
  return `${siteMetadata.siteUrl}/posts/${slug}`;
}

export default async function sitemap(): Promise<Route[]> {
  try {
    const response = await getFeed();
    const data: PostData[] = await response.data;

    const posts: Route[] = data.map(({ slug, createdAt }) => ({
      url: formatPostURL(slug),
      lastModified: createdAt,
    }));

    const routes: Route[] = [
      { url: `${siteMetadata.siteUrl}/`, lastModified: new Date().toISOString() },
      { url: `${siteMetadata.siteUrl}/projects`, lastModified: new Date().toISOString() },
      { url: `${siteMetadata.siteUrl}/posts`, lastModified: new Date().toISOString() }
    ];

    const sitemapData: Route[] = [...routes, ...posts];

    return sitemapData;
  } catch (error) {
    console.error("Error generating sitemap:", error);
    return [];
  }
}
