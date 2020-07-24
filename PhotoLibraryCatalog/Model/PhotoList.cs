using System.Collections.Generic;
using System.Linq;

namespace TuroPhoto.PhotoLibraryCatalog.Model
{
    class PhotoList : List<Photo>
    {
        public PhotoList() : base()
        {
        }

        public PhotoList(IEnumerable<Photo> collection) : base(collection)
        {
        }

        public IEnumerable<Photo> WhereHasLocation()
        {
            return this.Where(p => p.ImageMetaData?.GeoLocation != null);
        }
    }
}
