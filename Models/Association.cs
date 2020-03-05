//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assortie.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Association
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Association()
        {
            this.Adherents = new HashSet<Adherent>();
            this.HistoriquePaiements = new HashSet<HistoriquePaiement>();
            this.Sorties = new HashSet<Sortie>();
            this.SortieAdherents = new HashSet<SortieAdherent>();
        }
    
        public int IdAssociation { get; set; }
        public string Nom { get; set; }
        public decimal MontantCotisation { get; set; }
        public string Activite { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adherent> Adherents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriquePaiement> HistoriquePaiements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sortie> Sorties { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SortieAdherent> SortieAdherents { get; set; }
    }
}
