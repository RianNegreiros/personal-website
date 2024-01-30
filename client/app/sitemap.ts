import { MetadataRoute } from "next";
import { getFeed } from "./utils/api";
import siteMetadata from "./utils/siteMetaData";

interface PostData {
  slug: string;
  updatedAt: string;
}

export default async function sitemap(): Promise<MetadataRoute.Sitemap> {
  const response = await getFeed();
  const data: PostData[] = await response.data;

  return data.map(article => ({
    url: `${siteMetadata.siteUrl}/posts/${article.slug}`,
    lastModified: article.updatedAt,
    changeFrequency: 'weekly',
    priority: 0.5
  }));
}
